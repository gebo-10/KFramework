using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace kassets
{
    public enum State
    {
        Init,
        LoadBundle,
        LoadAsset,
        Loaded,
        Unload,
        Error
    }

    public abstract class AssetRequest : Reference
    {
        private State _state = State.Init;
        public string name;
        public Type assetType;
        public Action<AssetRequest> completed;

        public Object asset { get; internal set; }

        public AssetRequest()
        {
            asset = null;
            state = State.Init;
        }

        public State state
        {
            get { return _state; }
            protected set
            {
                _state = value;
                if (value == State.Loaded)
                {
                    Complete();
                }
            }
        }

        private void Complete()
        {
            if (completed != null)
            {
                completed(this);
                completed = null;
            }
        }

        public virtual bool isDone
        {
            get { return state == State.Loaded || state == State.Unload; }
        }

        public virtual float progress
        {
            get { return 1; }
        }

        public virtual string error { get; protected set; }

        internal abstract void Load();

        internal abstract void Unload();

        internal virtual bool Update()
        {
            if (!isDone)
                return true;
            Complete();
            return false;
        }
    }

    public class ManifestRequest : AssetRequest
    {
        private string assetName;
        private BundleRequest request;
        public Manifest manifest;
        public int version { get; private set; }

        public override float progress
        {
            get
            {
                if (isDone) return 1;

                if (state == State.Init) return 0;

                if (request == null) return 1;

                return request.progress;
            }
        }

        internal override void Load()
        {
            assetName = Path.GetFileName(name);
            if (Assets.runtimeMode)
            {
                BundleInfo bundleinfo = new BundleInfo();
                bundleinfo.name = "manifest";
                request = Assets.LoadBundleAsync(bundleinfo);
                state = State.LoadBundle;
            }
            else
            {
                manifest = new Manifest();
                state = State.Loaded;
            }
        }

        internal override bool Update()
        {
            if (!base.Update()) return false;

            if (state == State.Init) return true;

            if (request == null)
            {
                state = State.Loaded;
                error = "request == null";
                return false;
            }

            if (request.isDone)
            {
                if (request.assetBundle == null)
                {
                    error = "assetBundle == null";
                }
                else
                {
                    manifest = request.assetBundle.LoadAsset<Manifest>(assetName);
                    if (manifest == null)
                    {
                        error = "manifest == null";
                    }
                }

                state = State.Loaded;
                return false;
            }

            return true;
        }

        internal override void Unload()
        {
            if (request != null)
            {
                request.Release();
                request = null;
            }

            state = State.Unload;
        }
    }

    public class BundleAssetRequest : AssetRequest
    {
        protected readonly BundleInfo bundleInfo;
        protected BundleRequest BundleRequest;
        protected List<BundleRequest> children = new List<BundleRequest>();

        public BundleAssetRequest(BundleInfo info)
        {
            bundleInfo = info;
        }

        internal override void Load()
        {
            BundleRequest = Assets.LoadBundle(bundleInfo);
            var names = Assets.GetAllDependencies(bundleInfo);
            foreach (var item in names) children.Add(Assets.LoadBundle(item));
            var assetName = Path.GetFileName(name);
            var ab = BundleRequest.assetBundle;
            if (ab != null) asset = ab.LoadAsset(assetName, assetType);
            if (asset == null) error = "asset == null";
            state = State.Loaded;
        }

        internal override void Unload()
        {
            if (BundleRequest != null)
            {
                BundleRequest.Release();
                BundleRequest = null;
            }

            if (children.Count > 0)
            {
                foreach (var item in children) item.Release();
                children.Clear();
            }

            asset = null;
        }
    }

    public class BundleAssetRequestAsync : BundleAssetRequest
    {
        private AssetBundleRequest _request;

        public BundleAssetRequestAsync(BundleInfo info)
            : base(info)
        {
        }

        public override float progress
        {
            get
            {
                if (isDone) return 1;

                if (state == State.Init) return 0;

                if (_request != null) return _request.progress * 0.7f + 0.3f;

                if (BundleRequest == null) return 1;

                var value = BundleRequest.progress;
                var max = children.Count;
                if (max <= 0)
                    return value * 0.3f;

                for (var i = 0; i < max; i++)
                {
                    var item = children[i];
                    value += item.progress;
                }

                return value / (max + 1) * 0.3f;
            }
        }

        private bool OnError(BundleRequest bundleRequest)
        {
            error = bundleRequest.error;
            if (!string.IsNullOrEmpty(error))
            {
                state = State.Loaded;
                return true;
            }

            return false;
        }

        internal override bool Update()
        {
            if (!base.Update()) return false;

            if (state == State.Init) return true;

            if (_request == null)
            {
                if (!BundleRequest.isDone) return true;
                if (OnError(BundleRequest)) return false;

                for (var i = 0; i < children.Count; i++)
                {
                    var item = children[i];
                    if (!item.isDone) return true;
                    if (OnError(item)) return false;
                }

                var assetName = Path.GetFileName(name);
                _request = BundleRequest.assetBundle.LoadAssetAsync(assetName, assetType);
                if (_request == null)
                {
                    error = "request == null";
                    state = State.Loaded;
                    return false;
                }

                return true;
            }

            if (_request.isDone)
            {
                asset = _request.asset;
                state = State.Loaded;
                if (asset == null) error = "asset == null";
                return false;
            }

            return true;
        }

        internal override void Load()
        {
            BundleRequest = Assets.LoadBundleAsync(bundleInfo);
            var bundles = Assets.GetAllDependencies(bundleInfo);
            foreach (var item in bundles) children.Add(Assets.LoadBundleAsync(item));
            state = State.LoadBundle;
        }

        internal override void Unload()
        {
            _request = null;
            state = State.Unload;
            base.Unload();
        }
    }

    public class SceneAssetRequest : AssetRequest
    {
        protected readonly string sceneName;
        public BundleInfo bundleInfo;
        protected BundleRequest BundleRequest;
        protected List<BundleRequest> children = new List<BundleRequest>();

        public SceneAssetRequest(string path, bool addictive)
        {
            name = path;
            Assets.GetAssetBundleInfo(path, out bundleInfo);
            sceneName = Path.GetFileNameWithoutExtension(name);
            loadSceneMode = addictive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        }

        public LoadSceneMode loadSceneMode { get; protected set; }

        public override float progress
        {
            get { return 1; }
        }

        internal override void Load()
        {
            if (bundleInfo!=null)
            {
                BundleRequest = Assets.LoadBundle(bundleInfo);
                if (BundleRequest != null)
                {
                    var bundles = Assets.GetAllDependencies(bundleInfo);
                    foreach (var item in bundles) children.Add(Assets.LoadBundle(item));
                    SceneManager.LoadScene(sceneName, loadSceneMode);
                }
            }
            else
            {
                SceneManager.LoadScene(sceneName, loadSceneMode);
            }

            state = State.Loaded;
        }

        internal override void Unload()
        {
            if (BundleRequest != null)
                BundleRequest.Release();

            if (children.Count > 0)
            {
                foreach (var item in children) item.Release();
                children.Clear();
            }

            if (loadSceneMode == LoadSceneMode.Additive)
                if (SceneManager.GetSceneByName(sceneName).isLoaded)
                    SceneManager.UnloadSceneAsync(sceneName);

            BundleRequest = null;
            state = State.Unload;
        }
    }

    public class SceneAssetRequestAsync : SceneAssetRequest
    {
        private AsyncOperation _request;

        public SceneAssetRequestAsync(string path, bool addictive)
            : base(path, addictive)
        {
        }

        public override float progress
        {
            get
            {
                if (isDone) return 1;

                if (state == State.Init) return 0;

                if (_request != null) return _request.progress * 0.7f + 0.3f;

                if (BundleRequest == null) return 1;

                var value = BundleRequest.progress;
                var max = children.Count;
                if (max <= 0)
                    return value * 0.3f;

                for (var i = 0; i < max; i++)
                {
                    var item = children[i];
                    value += item.progress;
                }

                return value / (max + 1) * 0.3f;
            }
        }

        private bool OnError(BundleRequest bundleRequest)
        {
            error = bundleRequest.error;
            if (!string.IsNullOrEmpty(error))
            {
                state = State.Loaded;
                return true;
            }

            return false;
        }

        internal override bool Update()
        {
            if (!base.Update()) return false;

            if (state == State.Init) return true;

            if (_request == null)
            {
                if (BundleRequest == null)
                {
                    error = "bundle == null";
                    state = State.Loaded;
                    return false;
                }

                if (!BundleRequest.isDone) return true;

                if (OnError(BundleRequest)) return false;

                for (var i = 0; i < children.Count; i++)
                {
                    var item = children[i];
                    if (!item.isDone) return true;
                    if (OnError(item)) return false;
                }

                LoadScene();

                return true;
            }

            if (_request.isDone)
            {
                state = State.Loaded;
                return false;
            }

            return true;
        }

        private void LoadScene()
        {
            try
            {
                _request = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
                state = State.LoadAsset;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                error = e.Message;
                state = State.Loaded;
            }
        }

        internal override void Load()
        {
            if (bundleInfo != null)
            {
                BundleRequest = Assets.LoadBundleAsync(bundleInfo);
                var bundles = Assets.GetAllDependencies(bundleInfo);
                foreach (var item in bundles) children.Add(Assets.LoadBundleAsync(item));
                state = State.LoadBundle;
            }
            else
            {
                LoadScene();
            }
        }

        internal override void Unload()
        {
            base.Unload();
            _request = null;
        }
    }

    public class WebAssetRequest : AssetRequest
    {
        private UnityWebRequest _www;
        public string text { get; protected set; }

        public byte[] bytes { get; protected set; }
        public override float progress
        {
            get
            {
                if (isDone) return 1;
                if (state == State.Init) return 0;

                if (_www == null) return 1;

                return _www.downloadProgress;
            }
        }

        public override string error
        {
            get { return _www.error; }
        }


        internal override bool Update()
        {
            if (!base.Update()) return false;

            if (state == State.LoadAsset)
            {
                if (_www == null)
                {
                    error = "www == null";
                    return false;
                }

                if (!string.IsNullOrEmpty(_www.error))
                {
                    error = _www.error;
                    state = State.Loaded;
                    return false;
                }

                if (_www.isDone)
                {
                    GetAsset();
                    state = State.Loaded;
                    return false;
                }

                return true;
            }

            return true;
        }

        private void GetAsset()
        {
            if (assetType == typeof(Texture2D))
                asset = DownloadHandlerTexture.GetContent(_www);
            else if (assetType == typeof(AudioClip))
                asset = DownloadHandlerAudioClip.GetContent(_www);
            else if (assetType == typeof(TextAsset))
                text = _www.downloadHandler.text;
            else
                bytes = _www.downloadHandler.data;
        }

        internal override void Load()
        {
            if (assetType == typeof(AudioClip))
            {
                _www = UnityWebRequestMultimedia.GetAudioClip(name, AudioType.WAV);
            }
            else if (assetType == typeof(Texture2D))
            {
                _www = UnityWebRequestTexture.GetTexture(name);
            }
            else
            {
                _www = new UnityWebRequest(name);
                _www.downloadHandler = new DownloadHandlerBuffer();
            }

            _www.SendWebRequest();
            state = State.LoadAsset;
        }

        internal override void Unload()
        {
            if (asset != null)
            {
                Object.Destroy(asset);
                asset = null;
            }

            if (_www != null)
                _www.Dispose();

            bytes = null;
            text = null;
            state = State.Unload;
        }
    }

    public class BundleRequest : AssetRequest
    {
        public AssetBundle assetBundle
        {
            get { return asset as AssetBundle; }
            internal set { asset = value; }
        }

        internal override void Load()
        {
            asset = AssetBundle.LoadFromFile(name);
            if (assetBundle == null)
                error = name + " LoadFromFile failed.";
            state = State.Loaded;
        }

        internal override void Unload()
        {
            if (assetBundle == null)
                return;
            Debug.LogWarning("Unload Bundle: " + name);
            assetBundle.Unload(true);
            assetBundle = null;
            state = State.Unload;
        }
    }

    public class BundleRequestAsync : BundleRequest
    {
        private AssetBundleCreateRequest _request;

        public override float progress
        {
            get
            {
                if (isDone) return 1;
                if (state == State.Init) return 0;
                if (_request == null) return 1;
                return _request.progress;
            }
        }

        internal override bool Update()
        {
            if (!base.Update()) return false;

            if (state == State.LoadAsset)
                if (_request.isDone)
                {
                    assetBundle = _request.assetBundle;
                    if (assetBundle == null) error = string.Format("unable to load assetBundle:{0}", name);
                    state = State.Loaded;
                    return false;
                }

            return true;
        }

        internal override void Load()
        {
            if (_request == null)
            {
                _request = AssetBundle.LoadFromFileAsync(name);
                if (_request == null)
                {
                    error = name + " LoadFromFile failed.";
                    return;
                }

                state = State.LoadAsset;
            }
        }

        internal override void Unload()
        {
            _request = null;
            state = State.Unload;
            base.Unload();
        }
    }

    public class WebBundleRequest : BundleRequest
    {
        private UnityWebRequest _request;
        public bool cache;
        public Hash128 hash;

        public override float progress
        {
            get
            {
                if (isDone) return 1;
                if (state == State.Init) return 0;

                if (_request == null) return 1;

                return _request.downloadProgress;
            }
        }

        internal override void Load()
        {
            _request = cache
                ? UnityWebRequestAssetBundle.GetAssetBundle(name, hash)
                : UnityWebRequestAssetBundle.GetAssetBundle(name);
            _request.SendWebRequest();
            state = State.LoadBundle;
        }

        internal override void Unload()
        {
            if (_request != null)
            {
                _request.Dispose();
                _request = null;
            }

            state = State.Unload;
            base.Unload();
        }
    }

    public class EditorAssetRequest : AssetRequest
    {
        internal override void Load()
        {
#if UNITY_EDITOR
            asset = AssetDatabase.LoadAssetAtPath(name, assetType);
            if (asset == null)
            {
                error = "error! file not exist:" + name;
                Debug.LogError(error);
            }
            state = State.Loaded;
#endif
        }

        internal override void Unload()
        {
            if (asset == null)
                return;

            if (!(asset is GameObject))
                Resources.UnloadAsset(asset);

            asset = null;
            state = State.Unload;
        }
    }
}
using GameFramework.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace kassets
{
    public sealed class Assets : MonoBehaviour
    {
        /// <summary>
        /// config
        /// </summary>
        public static bool inited = false;
        public static bool runtimeMode = true;
        /// <summary>
        /// 5秒回收一次
        /// </summary>
        public static float cacheTime = 5;
        private static float passTime = 0;
        /// <summary>
        /// path
        /// </summary>
        public static string basePath { get => (Application.streamingAssetsPath + Path.DirectorySeparatorChar); }
        public static string updatePath { get => (Application.persistentDataPath + Path.DirectorySeparatorChar); }
        
        /// 元信息
        /// </summary>
        public static Manifest manifest;
        public static Dictionary<string, ModuleInfo> modules = new Dictionary<string, ModuleInfo>();
        public static Dictionary<string, BundleID> assetsToBundleID= new Dictionary<string, BundleID>();
        public static Dictionary<string, string> atlasPath = new Dictionary<string, string>();

        public static List<BundleRequest> Scripts = new List<BundleRequest>();
        #region API
        public static void Initialize(Action callback)
        {
            Clear();
            if (!runtimeMode)
            {
                inited = true;
                callback();
                return;
            }
            var request = new ManifestRequest { name="manifest"};
            request.completed = (AssetRequest req) =>
            {
                if(!string.IsNullOrEmpty(req.error))
                {
                    Debug.LogError("加载manifest 失败");
                    return;
                }
                ManifestRequest mreq = req as ManifestRequest;
                
                InitManifest(mreq.manifest);
                Assets.UnloadAsset(req);

                SpriteAtlasManager.atlasRequested += OnAtlasRequested;

                int i = 0;
                foreach (var scriptBundle in mreq.manifest.script)
                {
                    var script_req = LoadBundleAsync(scriptBundle);
                    script_req.completed += (AssetRequest result) =>
                    {
                        Scripts.Add(script_req);
                        if (++i == mreq.manifest.script.Length)
                        {
                            inited = true;
                            callback();
                        }
                    };
                }

                
            };
            AddAssetRequest(request);
        }

        public static void InitManifest(Manifest m)
        {
            manifest = m;
            assetsToBundleID.Clear();
            int moduleIndex = -1;
            foreach(var module in manifest.modules)
            {
                Debug.Log(module.name);
                moduleIndex++;
                int bundleIndex = -1;
                foreach (var bundle in module.bundles)
                {
                    bundleIndex++;
                    BundleID id = new BundleID();
                    id.module = moduleIndex;
                    id.bundle = bundleIndex;
                    foreach (var assets in bundle.assets)
                    {
                        assetsToBundleID.Add(assets, id);
                    }
                }
                modules.Add(module.name, module);
            }

            foreach(var atlats in manifest.atlas)
            {
                atlasPath.Add(atlats.name,atlats.path);
            }
        }

        private static void OnAtlasRequested(string tag, Action<SpriteAtlas> action)
        {
            LoadAssetAsync(atlasPath[tag], typeof(SpriteAtlas)).completed += (AssetRequest req) => {
                action(req.asset as SpriteAtlas);
            };
        }

        public static void Clear()
        {
            inited = false;
            manifest = null;
            passTime = 0;
            modules.Clear();
            assetsToBundleID.Clear();
            atlasPath.Clear();

            Scripts.Clear();

            _assets.Clear();
            _loadingAssets.Clear();

            ClearAllBundle();
        }

        public static bool CheckInit()
        {
            return inited;
        }

        public static AssetRequest LoadAssetAsync(string path, Type type)
        {
            return LoadAsset(path, type, true);
        }

        public static AssetRequest LoadAsset(string path, Type type)
        {
            return LoadAsset(path, type, false);
        }

        public static void UnloadAsset(AssetRequest asset)
        {
            asset.Release();
        }

        

        private void Update()
        {
            UpdateAssets();
            UpdateBundles();
            GC();
        }

        private static void GC()
        {
            passTime += Time.deltaTime;
            if (passTime >= cacheTime)
            {
                AssetsGC();
                BundleGC();
                passTime = 0;
            }
        }
        #endregion
        #region Module

        public static void LoadModule(string name)
        {
            if (!runtimeMode) return;

            var module = modules[name];
            foreach(var bundle in module.bundles)
            {
                LoadBundleAsync(bundle);
            }
        }

        public static void UnLoadModule(string name)
        {
            if (!runtimeMode) return;
            var module = modules[name];
            foreach (var bundle in module.bundles)
            {
                BundleRequest req;
                if (_bundles.TryGetValue(bundle, out req))
                {
                    UnloadBundle(req);
                }
            }
        }

        public static void SetModuleLocaled(string name)
        {
             PlayerPrefs.SetInt("DLC_" + name,1);
        }
        public static bool IsModuleLocaled(string name)
        {
            return PlayerPrefs.HasKey("DLC_" + name);
        }
        #endregion

        #region Assets
        /// <summary>
        /// 所有request 
        /// loadingassets 每帧检测 是否加载完成  资源卸载 间隔一秒或者5秒
        /// </summary>
        private static Dictionary<string, AssetRequest> _assets = new Dictionary<string, AssetRequest>();
        private static List<AssetRequest> _loadingAssets = new List<AssetRequest>();

        private static void CheckLoadingAssets()
        {
            for (var i = 0; i < _loadingAssets.Count; ++i)
            {
                var request = _loadingAssets[i];
                if (request.Update())
                    continue;
                _loadingAssets.RemoveAt(i);
                --i;
            }
        }

        private static void AssetsGC()
        {
            var needRemove = new List<string>();
            //两步删除
            foreach (var item in _assets)
            {
                if (item.Value.isDone && item.Value.IsUnused())
                {
                    needRemove.Add(item.Key);
                    item.Value.Unload();
                }
            }
            foreach (var item in needRemove)
            {
                _assets.Remove(item);
            }
        }

        private static void UpdateAssets()
        {
            CheckLoadingAssets();
        }

        private static void AddAssetRequest(AssetRequest request)
        {
            _assets.Add(request.name, request);
            _loadingAssets.Add(request);
            request.Load();
        }

        private static AssetRequest LoadAsset(string path, Type type, bool async)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Asset: LoadAsset invalid path");
                return null;
            }

            AssetRequest request;
            if (_assets.TryGetValue(path, out request))
            {
                request.Retain();
                _loadingAssets.Add(request);
                return request;
            }

            if (path.StartsWith("http://", StringComparison.Ordinal) ||
                       path.StartsWith("https://", StringComparison.Ordinal) ||
                       path.StartsWith("file://", StringComparison.Ordinal) ||
                       path.StartsWith("ftp://", StringComparison.Ordinal) ||
                       path.StartsWith("jar:file://", StringComparison.Ordinal))
            {
                request = new WebAssetRequest();
            }
            else
            {
                if (runtimeMode)
                {
                    BundleInfo assetBundleInfo;
                    if (GetAssetBundleInfo(path, out assetBundleInfo))
                    {
                        request = async
                            ? new BundleAssetRequestAsync(assetBundleInfo)
                            : new BundleAssetRequest(assetBundleInfo);
                    }
                    else
                    {
                        Debug.LogError("Can not get bundle, url= " + path);
                    }
                }
                else
                {
                    request = new EditorAssetRequest();
                }
            }

            request.name = path;
            request.assetType = type;
            AddAssetRequest(request);
            request.Retain();
            //Log(string.Format("LoadAsset:{0}", path));
            return request;
        }

        #endregion

        #region Bundles
        private static Dictionary<BundleInfo, BundleRequest> _bundles = new Dictionary<BundleInfo, BundleRequest>();
        private static List<BundleRequest> _loadingBundles = new List<BundleRequest>();

        public static bool GetAssetBundleInfo(string assetsName, out BundleInfo bundleInfo)
        {
            BundleID bundleID;
            if (!assetsToBundleID.TryGetValue(assetsName, out bundleID))
            {
                bundleInfo = null;
                return false;
            }
            bundleInfo = manifest.modules[bundleID.module].bundles[bundleID.bundle];
            return true;
        }

        internal static BundleInfo[] GetAllDependencies(BundleInfo bundleinfo)
        {
            List<BundleInfo> deps=new List<BundleInfo>();
            foreach(var id in bundleinfo.deps)
            {
                deps.Add(manifest.modules[id.module].bundles[id.bundle]);
            }

            return deps.ToArray();
        }

        internal static BundleRequest LoadBundle(BundleInfo bundleinfo)
        {
            return LoadBundle(bundleinfo, false);
        }

        internal static BundleRequest LoadBundleAsync(BundleInfo bundleinfo)
        {
            return LoadBundle(bundleinfo, true);
        }

        internal static void UnloadBundle(BundleRequest bundle)
        {
            bundle.Release();
        }  

        internal static BundleRequest LoadBundle(BundleInfo bundleinfo, bool asyncMode)
        {
            var url = GetDataPath(bundleinfo.name) +  bundleinfo.name;

            BundleRequest bundle;

            if (_bundles.TryGetValue(bundleinfo, out bundle))
            {
                bundle.Retain();
                _loadingBundles.Add(bundle);
                return bundle;
            }

            if (url.StartsWith("http://", StringComparison.Ordinal) ||
                url.StartsWith("https://", StringComparison.Ordinal) ||
                url.StartsWith("file://", StringComparison.Ordinal) ||
                url.StartsWith("ftp://", StringComparison.Ordinal))
                bundle = new WebBundleRequest();
            else
                bundle = asyncMode ? new BundleRequestAsync() : new BundleRequest();

            bundle.name = url;
            _bundles.Add(bundleinfo, bundle);

            bundle.Load();
            _loadingBundles.Add(bundle);
            Log("LoadBundle: " + url);
           

            bundle.Retain();
            return bundle;
        }

        private static void CheckLoadingBundle()
        {
            for (var i = 0; i < _loadingBundles.Count; i++)
            {
                var item = _loadingBundles[i];
                if (item.Update())
                    continue;
                _loadingBundles.RemoveAt(i);
                --i;
            }
        }

        private static void BundleGC()
        {
            var needRemove = new List<BundleInfo>();
            //两步删除
            foreach (var item in _bundles)
            {
                if (item.Value.isDone && item.Value.IsUnused())
                {
                    needRemove.Add(item.Key);
                    item.Value.Unload();
                }
            }
            foreach(var item in needRemove)
            {
                _bundles.Remove(item);
            }
        }
        private static void UpdateBundles()
        {
            CheckLoadingBundle();
        }
        
        private static void ClearAllBundle()
        {
            foreach(var req in _bundles)
            {
                req.Value.Unload();
            }
            //loading 的bundle 咋整

            _bundles.Clear();
            _loadingBundles.Clear();
        }
        #endregion

        private static void Log(string s)
        {
            Debug.Log(string.Format("{0}{1}", "[Assets]", s));
        }

        private static string GetDataPath(string bundleName)
        {
            if (string.IsNullOrEmpty(updatePath))
                return basePath;

            if (File.Exists(updatePath + bundleName))
                return updatePath;

            return basePath;
        }

        #region Script
        public static byte[] GetScript(string path)
        {
            if (runtimeMode) {
                string luaPath = "Assets/Lua/Script/" + path + ".bytes";
                foreach(var bundle in Scripts)
                {
                    if (bundle.assetBundle.Contains(luaPath))
                    {
                        return bundle.assetBundle.LoadAsset<TextAsset>(luaPath).bytes;
                    }
                }
                Debug.LogError("Cant find script: " + luaPath);
                return null;
            }
            else
            {
#if UNITY_EDITOR
                string luaPath = "Assets/Game/Script/" + path + ".lua";
                var assets = File.ReadAllBytes(luaPath);
                if (assets==null)
                {
                    Debug.LogError("error! file not exist:" + luaPath);
                    return null;
                }
                return assets;
#else
                return null;
#endif
            }
        }
        #endregion
    }
}
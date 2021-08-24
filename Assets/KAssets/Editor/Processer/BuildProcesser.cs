using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace kassets
{
    class BuildProcesser : IProcesser
    {
        static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();
        Dictionary<string, BundleID> bundleID=new Dictionary<string, BundleID>();
        static List<AtlasInfo> atlasInfo = new List<AtlasInfo>();
        AssetBundleManifest assetBundleManifest;

        public object Process()
        {
            Debug.Log("Build Process");
            maps.Clear();
            bundleID.Clear();
            atlasInfo.Clear();
            var manifest = BuildScript.manifest;
            var moduleInfoList = new List<ModuleInfo>();
            int module_index = 0;
            
            foreach (var module in BuildScript.buildRules.modules)
            {
                Debug.Log("Build module: " + module.name);
                var moduleInfo = new ModuleInfo();
                moduleInfo.name = module.name;
                moduleInfo.DLC = module.DLC;

                moduleInfoList.Add(moduleInfo);
                var bundleInfoList = new List<BundleInfo>();
                int bundle_index = 0;
                foreach (var rule in module.rules)
                {
                    var bundleNmae = module.name.ToLower() + "_" + rule.bundleName;
                    var build = GetBuildInfo(bundleNmae, rule.pattern, rule.path);
                    maps.Add(build);

                    var bundleInfo = new BundleInfo();
                    bundleInfo.name = bundleNmae;
                    bundleInfo.assets = build.assetNames;
                    bundleInfoList.Add(bundleInfo);

                    var id = new BundleID();
                    id.module = module_index++;
                    id.bundle = bundle_index++;
                    bundleID.Add(bundleNmae, id);
                }
                moduleInfo.bundles = bundleInfoList.ToArray();
            }

            manifest.modules = moduleInfoList.ToArray();
            manifest.atlas = atlasInfo.ToArray();

            assetBundleManifest = BuildPipeline.BuildAssetBundles(BuildScript.outputPath,
                    maps.ToArray(),
                    BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression,
                    BuildScript.target);

            UpdateManifest();
            return null;
        }

        static AssetBundleBuild GetBuildInfo(string bundleName, string pattern, string path)
        {
            string[] files = Directory.GetFiles(path, pattern, SearchOption.AllDirectories).Where(s => !s.EndsWith(".meta")).ToArray();
            if (files.Length == 0)
            {
                Debug.LogError("路径找不到指定模式文件"+ bundleName+ pattern+path);
                EditorUtility.DisplayDialog("Error", "路径找不到指定模式文件" + bundleName + pattern + path, "ok");
            }

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Replace('\\', '/');
                FileInfo info = new FileInfo(files[i]);
                if (info.Extension.Equals(".spriteatlas") )
                {
                    
                    atlasInfo.Add(new AtlasInfo { name = Path.GetFileNameWithoutExtension(info.Name), path = files[i] });
                }
            }
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = bundleName;
            build.assetNames = files;
            return build;
        }


        void UpdateManifest()
        {
            var bundles = assetBundleManifest.GetAllAssetBundles();
            var manifest = BuildScript.manifest;
            foreach (var module in manifest.modules)
            {
                foreach(var bundle in module.bundles)
                {
                    var deps = assetBundleManifest.GetAllDependencies(bundle.name);
                    var deps_ids = new List<BundleID>();
                    foreach(var dep in deps)
                    {
                        deps_ids.Add(bundleID[dep]);
                    }
                    bundle.deps = deps_ids.ToArray();

                    bundle.hash = assetBundleManifest.GetAssetBundleHash(bundle.name).ToString();
                    var fileInfo = new System.IO.FileInfo(BuildScript.outputPath+ bundle.name);
                    bundle.len = fileInfo.Length;
                }
            }
        }
    }
}

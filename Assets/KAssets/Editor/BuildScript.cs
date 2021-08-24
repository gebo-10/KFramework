using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace kassets
{
	public static class BuildScript
	{
		public static string outputPath  = "";
        public static string tmp = "Assets/Lua/";
        public static Manifest manifest;
        public static BuildRules buildRules;
        public static BuildTarget target;
        public static List<IProcesser> resourceProcessers = new List<IProcesser>
        {
            new InitProcesser(),
            new BuildProcesser(),
            new LuaProcesser(),
            new ManifestProcesser(),
            new CopyBundleProcesser(),
            new ClearProcesser(),
        };

        public static void Process(List<IProcesser> processers)
        {
            foreach(var p in processers)
            {
                p.Process();
            }
        }
        
        public static void BuildResource(BuildTarget t)
        {
            //var time = DateTime.Now.ToString ("yyyyMMdd-HHmmss");
            target = t;
            outputPath = "KAssets/"+target.ToString()+"/";
            Process(resourceProcessers);
        }

        internal static BuildRules GetBuildRules()
        {
            return GetAsset<BuildRules>("Assets/kassets/BuildRules.asset");
        }

        public static Manifest GetManifest()
        {
            return GetAsset<Manifest>("Assets/kassets/Manifest.asset");
        }

        private static T GetAsset<T>(string path) where T : ScriptableObject
        {
            var asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
            }

            return asset;
        }

        [MenuItem("Framework/Build Windows Resource", false, 100)]
        public static void BuildWindow()
        {
            BuildResource(BuildTarget.StandaloneWindows64);
        }
    }
}

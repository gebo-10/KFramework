using System.IO;
using UnityEditor;
using UnityEngine;
using LuaFramework;
namespace kassets 
{
    public class InitProcesser : IProcesser
    {
        public object Process()
        {
            BuildScript.buildRules= BuildScript.GetBuildRules();
            BuildScript.manifest = BuildScript.GetManifest();

            var manifest = BuildScript.manifest;
            manifest.modules = new ModuleInfo[0];
            manifest.script = new BundleInfo[0];

            CreateAssetBundleDirectory();
            return null;
        }
        public static string CreateAssetBundleDirectory()
        {
            //运行时目录
            if (Directory.Exists(Util.DataPath))
            {
                Directory.Delete(Util.DataPath, true);
            }

            //提交目录
            if (Directory.Exists(BuildScript.outputPath))
            {
                Directory.Delete(BuildScript.outputPath, true);
            }
            //System.Threading.Thread.Sleep(500);
            Directory.CreateDirectory(BuildScript.outputPath);

            //streamingAssets目录
            string streamPath = Application.streamingAssetsPath;
            if (Directory.Exists(streamPath))
            {
                Directory.Delete(streamPath, true);
            }
            //System.Threading.Thread.Sleep(500);
            Directory.CreateDirectory(streamPath);

            //Tmp目录
            string tmp = BuildScript.tmp;
            if (Directory.Exists(tmp))
            {
                Directory.Delete(tmp, true);
            }
            //System.Threading.Thread.Sleep(500);
            Directory.CreateDirectory(tmp);

            AssetDatabase.Refresh();
            return BuildScript.outputPath;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace kassets
{
    class ManifestProcesser : IProcesser
    {
        public object Process()
        {
            EditorUtility.SetDirty(BuildScript.manifest);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var builds = new[] {
                new AssetBundleBuild {
                    assetNames = new[] { AssetDatabase.GetAssetPath (BuildScript.manifest), },
                    assetBundleName =  "manifest"
                }
            };

            BuildPipeline.BuildAssetBundles(BuildScript.outputPath, 
                builds, 
                BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression,
                BuildScript.target);
            return null;
        }
    }
}

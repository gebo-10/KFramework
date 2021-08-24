using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace kassets
{
    class LuaProcesser : IProcesser
    {
        List<AssetBundleBuild> maps = new List<AssetBundleBuild>();
        AssetBundleManifest assetBundleManifest;
        public object Process()
        {
            maps.Clear();
            CopyScript("Assets/Game/Script/",BuildScript.tmp+"Script/");
            AssetDatabase.Refresh();
            BuildBundle();
            UpdateManifest();
            return null;
        }
        public void CopyScript(string src, string dist)
        {
            if(Directory.Exists(dist))
                Directory.Delete(dist);
            CopyDirectory(src, dist,true);
        }
        public bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        if (!fls.EndsWith(".lua")) continue;
                        FileInfo flinfo = new FileInfo(fls);
                        
                        flinfo.CopyTo(DestinationPath + Path.ChangeExtension(flinfo.Name, ".bytes"), overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        public void BuildBundle()
        {
            var manifest = BuildScript.manifest;
            
            var bundleInfoList = new List<BundleInfo>();
            foreach (var rule in BuildScript.buildRules.Script)
            {
                
                var bundleName="script_" + rule.bundleName;
                
                var build=AddBuildInfo(bundleName, rule.pattern, BuildScript.tmp + rule.path);
                maps.Add(build);

                var bundleInfo = new BundleInfo();
                //bundleInfo.assets = build.assetNames; //lua不需要记录assets
                bundleInfo.name = bundleName;
                bundleInfoList.Add(bundleInfo);
            }
            manifest.script = bundleInfoList.ToArray();

            assetBundleManifest= BuildPipeline.BuildAssetBundles(BuildScript.outputPath,
                   maps.ToArray(),
                   BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression,
                   BuildScript.target);
        }

        AssetBundleBuild AddBuildInfo(string bundleName, string pattern, string path)
        {
            string[] files = Directory.GetFiles(path, pattern, SearchOption.AllDirectories).Where(s => !s.EndsWith(".meta")).ToArray();
            if (files.Length == 0)
            {
                Debug.LogError("路径找不到指定模式文件" + bundleName + pattern + path);
                EditorUtility.DisplayDialog("Error", "路径找不到指定模式文件" + bundleName + pattern + path, "ok");
            }

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Replace('\\', '/');
            }
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = bundleName;
            build.assetNames = files;
            return build;
        }



        void UpdateManifest()
        {

            foreach (var bundle in BuildScript.manifest.script)
            {
                bundle.hash = assetBundleManifest.GetAssetBundleHash(bundle.name).ToString();
                var fileInfo = new System.IO.FileInfo(BuildScript.outputPath + bundle.name);
                bundle.len = fileInfo.Length;
            }
    
        }

        //static void HandleLuaFile(bool is_x64, bool byteMode)
        //{
        //    string resPath = AppDataPath + "/StreamingAssets/";
        //    string luaPath = resPath + "/lua/x32/";
        //    if (is_x64)
        //    {
        //        luaPath = resPath + "/lua/x64/";
        //    }
        //    //----------复制Lua文件---------------- 
        //    if (!Directory.Exists(luaPath))
        //    {
        //        Directory.CreateDirectory(luaPath);
        //    }
        //    string[] luaPaths = { AppDataPath + "/Game/lua/",
        //                      AppDataPath + "/ToLuaSharp/Tolua/Lua/" };

        //    for (int i = 0; i < luaPaths.Length; i++)
        //    {
        //        paths.Clear(); files.Clear();
        //        string luaDataPath = luaPaths[i].ToLower();
        //        Recursive(luaDataPath);
        //        int n = 0;
        //        foreach (string f in files)
        //        {
        //            if (f.EndsWith(".meta")) continue;
        //            string newfile = f.Replace(luaDataPath, "");
        //            string newpath = luaPath + newfile;
        //            string path = Path.GetDirectoryName(newpath);
        //            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        //            if (File.Exists(newpath))
        //            {
        //                File.Delete(newpath);
        //            }
        //            if (byteMode)
        //            {
        //                EncodeLuaFile(is_x64, f, newpath);
        //            }
        //            else
        //            {
        //                File.Copy(f, newpath, true);
        //            }
        //            //UpdateProgress(n++, files.Count, newpath);
        //        }
        //    }
        //    EditorUtility.ClearProgressBar();
        //    AssetDatabase.Refresh();
        //}


        //public static void EncodeLuaFile(bool is_x64, string srcFile, string outFile)
        //{
        //    if (!srcFile.ToLower().EndsWith(".lua"))
        //    {
        //        File.Copy(srcFile, outFile, true);
        //        return;
        //    }
        //    bool isWin = true;
        //    string luaexe = string.Empty;
        //    string args = string.Empty;
        //    string exedir = string.Empty;
        //    string currDir = Directory.GetCurrentDirectory();
        //    if (Application.platform == RuntimePlatform.WindowsEditor)
        //    {
        //        isWin = true;
        //        luaexe = "luajit.exe";
        //        args = "-b -g " + srcFile + " " + outFile;
        //        exedir = AppDataPath.Replace("assets", "") + "luajit32/";
        //        if (is_x64)
        //        {
        //            exedir = AppDataPath.Replace("assets", "") + "luajit64/";
        //        }
        //    }
        //    else if (Application.platform == RuntimePlatform.OSXEditor)
        //    {
        //        isWin = false;
        //        luaexe = "./luajit";
        //        args = "-b -g " + srcFile + " " + outFile;
        //        exedir = AppDataPath.Replace("assets", "") + "luajit_mac/";
        //    }
        //    Directory.SetCurrentDirectory(exedir);
        //    ProcessStartInfo info = new ProcessStartInfo();
        //    info.FileName = luaexe;
        //    info.Arguments = args;
        //    info.WindowStyle = ProcessWindowStyle.Hidden;
        //    info.UseShellExecute = isWin;
        //    info.ErrorDialog = true;
        //    Util.Log(info.FileName + " " + info.Arguments);

        //    Process pro = Process.Start(info);
        //    pro.WaitForExit();
        //    Directory.SetCurrentDirectory(currDir);
        //}

        /// <summary>
        /// 处理Lua代码包
        /// </summary>
        //static void HandleLuaBundle(bool is_x64)
        //{
        //    string streamDir = Application.dataPath + "/" + AppConst.LuaTempDir;
        //    if (is_x64)
        //    {
        //        streamDir += "x64/";
        //    }
        //    else
        //    {
        //        streamDir += "x32/";
        //    }

        //    if (!Directory.Exists(streamDir)) Directory.CreateDirectory(streamDir);

        //    string[] srcDirs = { AppDataPath + "/Game/lua/", AppDataPath + "/ToLuaSharp/Tolua/Lua/" };
        //    for (int i = 0; i < srcDirs.Length; i++)
        //    {
        //        if (AppConst.LuaByteMode)
        //        {
        //            string sourceDir = srcDirs[i];
        //            string[] files = Directory.GetFiles(sourceDir, "*.lua", SearchOption.AllDirectories);
        //            int len = sourceDir.Length;

        //            if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\')
        //            {
        //                --len;
        //            }
        //            for (int j = 0; j < files.Length; j++)
        //            {
        //                string str = files[j].Remove(0, len);
        //                string dest = streamDir + str + ".bytes";
        //                string dir = Path.GetDirectoryName(dest);
        //                Directory.CreateDirectory(dir);
        //                EncodeLuaFile(is_x64, files[j], dest);
        //            }
        //        }
        //        else
        //        {
        //            ToLuaMenu.CopyLuaBytesFiles(srcDirs[i], streamDir);
        //        }
        //    }
        //    //string[] dirs = Directory.GetDirectories(streamDir, "*", SearchOption.AllDirectories);
        //    //for (int i = 0; i < dirs.Length; i++) {
        //    //    string name = dirs[i].Replace(streamDir, string.Empty);
        //    //    name = name.Replace('\\', '_').Replace('/', '_');
        //    //    name = "lua/lua_" + name.ToLower() + AppConst.ExtName;

        //    //    string path = "Assets" + dirs[i].Replace(Application.dataPath, "");
        //    //    AddBuildMap(name, "*.bytes", path);
        //    //}

        //    if (is_x64)
        //    {
        //        AddBuildMap("lua64" + AppConst.ExtName, "*", "Assets/Lua/x64");
        //    }
        //    else
        //    {
        //        AddBuildMap("lua32" + AppConst.ExtName, "*", "Assets/Lua/x32");
        //    }

        //    AssetDatabase.Refresh();
        //}
    }
}

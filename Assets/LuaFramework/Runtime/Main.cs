using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace LuaFramework
{
    class Main :MonoBehaviour
    {
        public TextAsset bootSource;
        private void Start()
        {
            StartCoroutine(Init());
        }
        IEnumerator Init()
        {
            yield return new WaitForEndOfFrame();
            GameEntry.Init();
            GameEntry.DownloadExt.Init();
            var bootPath = Util.DataPath+"boot.txt";
            string boot;
            if(Directory.Exists(Util.DataPath) && File.Exists(bootPath))
            {
                boot = bootSource.text;
            }
            else
            {
                var assets=Resources.Load<TextAsset>("boot");
                boot = assets.text;
            }
            GameEntry.Lua.StartBoot(ref boot);

            yield return null;
        }
    }
}

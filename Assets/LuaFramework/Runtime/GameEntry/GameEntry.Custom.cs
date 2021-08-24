using kassets;
using UnityEngine;

namespace LuaFramework
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry
    {
        public static LuaComponent Lua
        {
            get;
            private set;
        }

        public static DownloadExt DownloadExt
        {
            get;
            private set;
        }

        public static NetworkManager networkManager
        {
            get;
            private set;
        }

        public static TimerManager Timer
        {
            get;
            private set;
        }

        //public static PanelManager panelManager
        //{
        //    get;
        //    private set;
        //}

        //public static ResourceManager resourceManager
        //{
        //    get;
        //    private set;
        //}
        //public static SDKManager sdkManager
        //{
        //    get;
        //    private set;
        //}
        //public static SoundManager soundManager
        //{
        //    get;
        //    private set;
        //}


        //public static PayManager payManager
        //{
        //    get;
        //    private set;
        //}

        //public static FileManager fileManager
        //{
        //    get;
        //    private set;
        //}

        private static void InitCustomComponents()
        {
            Lua = UnityGameFramework.Runtime.GameEntry.GetComponent<LuaComponent>();
            DownloadExt = UnityGameFramework.Runtime.GameEntry.GetComponent<DownloadExt>();
            networkManager = UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkManager>();
            Timer = UnityGameFramework.Runtime.GameEntry.GetComponent<TimerManager>();
            //panelManager = UnityGameFramework.Runtime.GameEntry.GetComponent<PanelManager>();
            //resourceManager = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceManager>();
            //sdkManager = UnityGameFramework.Runtime.GameEntry.GetComponent<SDKManager>();
            //soundManager = UnityGameFramework.Runtime.GameEntry.GetComponent<SoundManager>();
            //payManager = UnityGameFramework.Runtime.GameEntry.GetComponent<PayManager>();
            //fileManager= UnityGameFramework.Runtime.GameEntry.GetComponent<FileManager>();
        }
    }
}

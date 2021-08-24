using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace LuaFramework
{
    public class DownloadItem
    {
        public int id;
        public int state;
        public string url;
        public string path;
        public int current;
        public int delta;
        public Action<DownloadItem> callback;
    }

    public class DownloadExt: GameFrameworkComponent
    {
        public void Init()
        {
            GameEntry.Event.Subscribe(DownloadUpdateEventArgs.EventId, OnDownloadUpdate);
            GameEntry.Event.Subscribe(DownloadFailureEventArgs.EventId, OnDownloadFail);
            GameEntry.Event.Subscribe(DownloadSuccessEventArgs.EventId, OnDownloadSuccess);
        }

        public float CurrentSpeed => GameEntry.Download.CurrentSpeed;
        public void AddDownload(string path, string url, Action<DownloadItem> callback)
        {
            DownloadItem item = new DownloadItem {
                state = 0,
                url = url,
                path = path,
                callback = callback,
                current=0,
                delta=0,
            };
            item.id = GameEntry.Download.AddDownload(path, url, item);
        }
        private void OnDownloadUpdate(object sender, GameEventArgs e)
        {
            DownloadUpdateEventArgs ne = (DownloadUpdateEventArgs)e;
            var item = ne.UserData as DownloadItem;
            if (item == null) return;      
            item.delta = ne.CurrentLength - item.current;
            item.current = ne.CurrentLength;
            item.callback(item);
        }

        private void OnDownloadSuccess(object sender, GameEventArgs e)
        {
            DownloadSuccessEventArgs ne = (DownloadSuccessEventArgs)e;
            var item = ne.UserData as DownloadItem;
            if (item == null) return;
            item.delta = ne.CurrentLength - item.current;
            item.current = ne.CurrentLength;
            item.state = 1;
            item.callback(item);
        }

        private void OnDownloadFail(object sender, GameEventArgs e)
        {
            DownloadFailureEventArgs ne = (DownloadFailureEventArgs)e;
            var item = ne.UserData as DownloadItem;
            if (item == null) return;
            item.state = -1;
            item.callback(item);
        }
    }
}

using UnityEngine;
using System;
using System.Collections;
using System.Net;
using UnityEngine.Networking;
using System.Text;
using UnityGameFramework.Runtime;
using GameFramework.Network;

namespace LuaFramework {
    public class NetworkManager : GameFrameworkComponent
    {
        private INetworkChannel channel;
        private void Init()
        {
            channel = GameEntry.Network.CreateNetworkChannel("main", ServiceType.Tcp, new NetworkHelper());
            channel.RegisterHandler(new PacketHandler());
        }

        /// <summary>
        /// 发送链接请求
        /// </summary>
        public void SendConnect()
        {
            if (channel == null) Init();
            IPAddress[] address = Dns.GetHostAddresses(AppConst.SocketAddress);
            channel.Connect(address[0], AppConst.SocketPort);
        }

        /// <summary>
        /// 发送链接请求
        /// </summary>
        public void SendConnect(string host, int port)
        {
            if (channel == null) Init();
            IPAddress[] address = Dns.GetHostAddresses(host);
            channel.Connect(address[0], port);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendProto(byte[]  buffer, string protoNmae)
        {
            ProtoPacket packet = new ProtoPacket();
            packet.protoName = protoNmae;
            packet.content = buffer;
            channel.Send(packet);
        }

        /// <summary>
        /// 关闭链接
        /// </summary>
        public void CloseConnect()
        {
            channel.Close();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        void OnDestroy()
        {
            GameEntry.Network.DestroyNetworkChannel("main");
            Debug.Log("~NetworkManager was destroy");
        }

        /// //////////////////////////////////////////////////////////////////////////////
        public void HttpGet(String url, Action<int, string> cb)
        {
            StartCoroutine(CoHttpGet(url,cb));
        }

        IEnumerator CoHttpGet(String url, Action<int, string> func)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            int error = 0;
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                error = 1;
                func(error, "");
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                string str = Encoding.UTF8.GetString(results);
                func(error, str);
            }
        }

        public void HttpPost(String url,String json, Action<int, string> cb)
        {
            StartCoroutine(CoHttpPost(url, json, cb));
        }

        IEnumerator CoHttpPost(String url, String json, Action<int, string> func)
        {
            var www = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();
        
            int error = 0;
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                error = 1;
                if (func != null) func(error, "");
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                string str = System.Text.Encoding.Default.GetString(results);
                if (func != null) func(error, str);
            }
        }


        public void HttpGetImage(String url, Action<int, Sprite> cb)
        {
            StartCoroutine(CoHttpGetImage(url, cb));
        }

        IEnumerator CoHttpGetImage(String url, Action<int, Sprite> func)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            www.downloadHandler = texDl;

            yield return www.SendWebRequest();
            int error = 0;
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                error = 1;
                if (func != null) func(error, null);
            }
            else
            {
                Texture2D t2d = texDl.texture;
                Sprite s = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
                if (func != null) func(error, s);
            }
        }

        public void Upload(string url, byte[] data, string json, Action<int, string> cb)
        {
            StartCoroutine(CoUpload(url, data, json, cb));
        }
        IEnumerator CoUpload(string url ,byte[] data, string json, Action<int, string> cb)
        {
            WWWForm form = new WWWForm();
            form.AddBinaryData("data", data);
            form.AddField("info", json);
            using (UnityWebRequest www = UnityWebRequest.Post(url, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    cb(-1,null);
                }
                else
                {
                    string text = www.downloadHandler.text;
                    cb(0, text);
                }
            }
        }
    }
}
using GameFramework.Event;
using kassets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public delegate void Main(int i);
public class test : MonoBehaviour
{
   
    void Start()
    {
        var ev =  GameEntry.GetComponent<EventComponent>();
        var lua = GameEntry.GetComponent<LuaComponent>();


        Assets.Initialize(()=> {
            Assets.LoadModule("Common");
            Assets.LoadModule("Main");
            Assets.LoadModule("BlackJack");
            StartCoroutine( TestLoad());
            lua.StartMain();
        });
        
    }

    IEnumerator TestLoad()
    {
        yield return new WaitForSeconds(5);
        Assets.LoadAssetAsync("Assets/Game/Main/Panel/PanelMain.prefab", typeof(GameObject)).completed += delegate (AssetRequest request)
        {
            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
                //return;
                //yield break;
            }
            var go = Instantiate(request.asset as GameObject);
            go.name = request.asset.name;
            //Destroy(go, 3);
            /// 设置关注对象后，只需要释放一次 
            /// 这里如果之前没有调用 Require，下一帧这个资源就会被回收
            //request.Release();
            var canvas = GameObject.Find("Canvas");
            go.transform.SetParent(canvas.transform);


           Assets.LoadAssetAsync("Assets/Game/Common/Atlas/Image.spriteatlas", typeof(SpriteAtlas)).completed += delegate (AssetRequest req)
            {
                var img = GameObject.Find("Image");
                var ats = req.asset as SpriteAtlas;
                img.GetComponent<Image>().sprite = ats.GetSprite("button-text-clear");
                //req.Release();
                //request.Release();

                Assets.UnLoadModule("Common");
                Assets.UnLoadModule("Main");
                Assets.UnLoadModule("BlackJack");
            };
        };
        yield break;
    }
}

using kassets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

public enum MessageType
{
    Unknow,
    Proto,
    OnConnect,
    OnDisconnect,
    OnConnectFail,
    OnException,
}

public class LuaComponent : GameFrameworkComponent
{
    private LuaEnv _luaEnv = null;
    Action update=()=> {};
    Action<MessageType, LuaTable> onMessage;

    void Start()
    {
        _luaEnv = new LuaEnv();
        _luaEnv.AddBuildin("rapidjson", LuaDLL.Lua.LoadRapidJson);
        _luaEnv.AddBuildin("cjson", LuaDLL.Lua.LoadCJson);
        _luaEnv.AddBuildin("pb", LuaDLL.Lua.LoadPB);
        _luaEnv.AddLoader(Loader);
    }

    public void StartBoot(ref string bootScript)
    {
        _luaEnv.DoString(bootScript);
        Action boot = _luaEnv.Global.Get<Action>("Bootstrip");
        update = _luaEnv.Global.Get<Action>("Update");
        boot();
    }

    public void StartMain()
    {
        _luaEnv.DoString("require 'main.main'");
        Action main = _luaEnv.Global.Get<Action>("Main");
        update = _luaEnv.Global.Get<Action>("Update");
        onMessage= _luaEnv.Global.Get<Action<MessageType, LuaTable> >("OnMessage");
        main();
    }

    void Update()
    {
        update();
    }

    private static byte[] Loader(ref string fileName)
    {
        var path = fileName.Replace('.', '/');
        return Assets.GetScript(path);
    }

    void OnDestroy()
    {
        update = null;
        onMessage = null;
        _luaEnv.Dispose();
    }

    public void DoString(string script)
    {
        _luaEnv.DoString(script);
    }

    public void Message(MessageType type, params ValueTuple<string, dynamic>[]  param)
    {
        LuaTable table = _luaEnv.NewTable();
        foreach(var item in param)
        {
            table.Set(item.Item1, item.Item2);
        }
        onMessage(type, table);
    }
}

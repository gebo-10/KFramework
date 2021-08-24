#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    
    public class UiTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UiType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UiType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UiType), L, null, 13, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Control", UiType.Control);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Button", UiType.Button);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Text", UiType.Text);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Image", UiType.Image);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DScrollView", UiType.DScrollView);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ScrollView", UiType.ScrollView);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InputField", UiType.InputField);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Slider", UiType.Slider);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Toggle", UiType.Toggle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ToggleGroup", UiType.ToggleGroup);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Dropdown", UiType.Dropdown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Tab", UiType.Tab);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UiType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUiType(L, (UiType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Control"))
                {
                    translator.PushUiType(L, UiType.Control);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Button"))
                {
                    translator.PushUiType(L, UiType.Button);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Text"))
                {
                    translator.PushUiType(L, UiType.Text);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Image"))
                {
                    translator.PushUiType(L, UiType.Image);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DScrollView"))
                {
                    translator.PushUiType(L, UiType.DScrollView);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ScrollView"))
                {
                    translator.PushUiType(L, UiType.ScrollView);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InputField"))
                {
                    translator.PushUiType(L, UiType.InputField);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Slider"))
                {
                    translator.PushUiType(L, UiType.Slider);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Toggle"))
                {
                    translator.PushUiType(L, UiType.Toggle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ToggleGroup"))
                {
                    translator.PushUiType(L, UiType.ToggleGroup);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Dropdown"))
                {
                    translator.PushUiType(L, UiType.Dropdown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Tab"))
                {
                    translator.PushUiType(L, UiType.Tab);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UiType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UiType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class MessageTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(MessageType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(MessageType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(MessageType), L, null, 7, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unknow", MessageType.Unknow);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Proto", MessageType.Proto);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OnConnect", MessageType.OnConnect);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OnDisconnect", MessageType.OnDisconnect);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OnConnectFail", MessageType.OnConnectFail);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OnException", MessageType.OnException);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(MessageType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushMessageType(L, (MessageType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Unknow"))
                {
                    translator.PushMessageType(L, MessageType.Unknow);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Proto"))
                {
                    translator.PushMessageType(L, MessageType.Proto);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OnConnect"))
                {
                    translator.PushMessageType(L, MessageType.OnConnect);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OnDisconnect"))
                {
                    translator.PushMessageType(L, MessageType.OnDisconnect);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OnConnectFail"))
                {
                    translator.PushMessageType(L, MessageType.OnConnectFail);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OnException"))
                {
                    translator.PushMessageType(L, MessageType.OnException);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for MessageType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for MessageType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class Reporter_LogTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Reporter._LogType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Reporter._LogType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Reporter._LogType), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Assert", Reporter._LogType.Assert);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Error", Reporter._LogType.Error);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Exception", Reporter._LogType.Exception);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Log", Reporter._LogType.Log);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Warning", Reporter._LogType.Warning);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Reporter._LogType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushReporter_LogType(L, (Reporter._LogType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Assert"))
                {
                    translator.PushReporter_LogType(L, Reporter._LogType.Assert);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Error"))
                {
                    translator.PushReporter_LogType(L, Reporter._LogType.Error);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Exception"))
                {
                    translator.PushReporter_LogType(L, Reporter._LogType.Exception);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Log"))
                {
                    translator.PushReporter_LogType(L, Reporter._LogType.Log);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Warning"))
                {
                    translator.PushReporter_LogType(L, Reporter._LogType.Warning);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Reporter._LogType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Reporter._LogType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}
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
    public class LuaFrameworkTimerManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LuaFramework.TimerManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnTimeOut", _m_OnTimeOut);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsRuning", _m_IsRuning);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopTimer", _m_StopTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Delay", _m_Delay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Loop", _m_Loop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddTimer", _m_AddTimer);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "timers", _g_get_timers);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "timers", _s_set_timers);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					LuaFramework.TimerManager gen_ret = new LuaFramework.TimerManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to LuaFramework.TimerManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnTimeOut(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.TimerManager gen_to_be_invoked = (LuaFramework.TimerManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    LuaFramework.TimerItem _item = (LuaFramework.TimerItem)translator.GetObject(L, 2, typeof(LuaFramework.TimerItem));
                    
                    gen_to_be_invoked.OnTimeOut( _item );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsRuning(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.TimerManager gen_to_be_invoked = (LuaFramework.TimerManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint _id = LuaAPI.xlua_touint(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.IsRuning( _id );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.TimerManager gen_to_be_invoked = (LuaFramework.TimerManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint _id = LuaAPI.xlua_touint(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.StopTimer( _id );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Delay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.TimerManager gen_to_be_invoked = (LuaFramework.TimerManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _timeout = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action _action = translator.GetDelegate<System.Action>(L, 3);
                    
                        uint gen_ret = gen_to_be_invoked.Delay( _timeout, _action );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Loop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.TimerManager gen_to_be_invoked = (LuaFramework.TimerManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _timeout = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action _action = translator.GetDelegate<System.Action>(L, 3);
                    
                        uint gen_ret = gen_to_be_invoked.Loop( _timeout, _action );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.TimerManager gen_to_be_invoked = (LuaFramework.TimerManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _times = LuaAPI.xlua_tointeger(L, 2);
                    float _timeout = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _action = translator.GetDelegate<System.Action>(L, 4);
                    
                        uint gen_ret = gen_to_be_invoked.AddTimer( _times, _timeout, _action );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaFramework.TimerManager gen_to_be_invoked = (LuaFramework.TimerManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.timers);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_timers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaFramework.TimerManager gen_to_be_invoked = (LuaFramework.TimerManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.timers = (System.Collections.Generic.Dictionary<uint, LuaFramework.TimerItem>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<uint, LuaFramework.TimerItem>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}

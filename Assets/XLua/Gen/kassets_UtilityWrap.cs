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
    public class kassetsUtilityWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(kassets.Utility);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 6, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMD5Hash", _m_GetMD5Hash_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "VerifyMd5Hash", _m_VerifyMd5Hash_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCRC32Hash", _m_GetCRC32Hash_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCrc", _m_GetCrc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "VerifyCrc32Hash", _m_VerifyCrc32Hash_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "kassets.Utility does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMD5Hash_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = kassets.Utility.GetMD5Hash( _input );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.IO.Stream>(L, 1)) 
                {
                    System.IO.Stream _input = (System.IO.Stream)translator.GetObject(L, 1, typeof(System.IO.Stream));
                    
                        string gen_ret = kassets.Utility.GetMD5Hash( _input );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to kassets.Utility.GetMD5Hash!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_VerifyMd5Hash_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _hash = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = kassets.Utility.VerifyMd5Hash( _input, _hash );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCRC32Hash_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.IO.Stream>(L, 1)) 
                {
                    System.IO.Stream _input = (System.IO.Stream)translator.GetObject(L, 1, typeof(System.IO.Stream));
                    
                        string gen_ret = kassets.Utility.GetCRC32Hash( _input );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        string gen_ret = kassets.Utility.GetCRC32Hash( _bytes );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = kassets.Utility.GetCRC32Hash( _input );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to kassets.Utility.GetCRC32Hash!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCrc_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        uint gen_ret = kassets.Utility.GetCrc( _bytes );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_VerifyCrc32Hash_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _hash = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = kassets.Utility.VerifyCrc32Hash( _input, _hash );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}

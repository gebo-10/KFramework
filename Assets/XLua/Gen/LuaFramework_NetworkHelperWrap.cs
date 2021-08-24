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
    public class LuaFrameworkNetworkHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LuaFramework.NetworkHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 1, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PrepareForConnecting", _m_PrepareForConnecting);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendHeartBeat", _m_SendHeartBeat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DeserializePacket", _m_DeserializePacket);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DeserializePacketHeader", _m_DeserializePacketHeader);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Serialize", _m_Serialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Shutdown", _m_Shutdown);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "PacketHeaderLength", _g_get_PacketHeaderLength);
            
			
			
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
					
					LuaFramework.NetworkHelper gen_ret = new LuaFramework.NetworkHelper();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to LuaFramework.NetworkHelper constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.NetworkHelper gen_to_be_invoked = (LuaFramework.NetworkHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameFramework.Network.INetworkChannel _networkChannel = (GameFramework.Network.INetworkChannel)translator.GetObject(L, 2, typeof(GameFramework.Network.INetworkChannel));
                    
                    gen_to_be_invoked.Initialize( _networkChannel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PrepareForConnecting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.NetworkHelper gen_to_be_invoked = (LuaFramework.NetworkHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PrepareForConnecting(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendHeartBeat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.NetworkHelper gen_to_be_invoked = (LuaFramework.NetworkHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.SendHeartBeat(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeserializePacket(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.NetworkHelper gen_to_be_invoked = (LuaFramework.NetworkHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameFramework.Network.IPacketHeader _packetHeader = (GameFramework.Network.IPacketHeader)translator.GetObject(L, 2, typeof(GameFramework.Network.IPacketHeader));
                    System.IO.Stream _source = (System.IO.Stream)translator.GetObject(L, 3, typeof(System.IO.Stream));
                    object _customErrorData;
                    
                        GameFramework.Network.Packet gen_ret = gen_to_be_invoked.DeserializePacket( _packetHeader, _source, out _customErrorData );
                        translator.Push(L, gen_ret);
                    translator.PushAny(L, _customErrorData);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeserializePacketHeader(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.NetworkHelper gen_to_be_invoked = (LuaFramework.NetworkHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.IO.Stream _source = (System.IO.Stream)translator.GetObject(L, 2, typeof(System.IO.Stream));
                    object _customErrorData;
                    
                        GameFramework.Network.IPacketHeader gen_ret = gen_to_be_invoked.DeserializePacketHeader( _source, out _customErrorData );
                        translator.PushAny(L, gen_ret);
                    translator.PushAny(L, _customErrorData);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Serialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.NetworkHelper gen_to_be_invoked = (LuaFramework.NetworkHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameFramework.Network.Packet _packet = (GameFramework.Network.Packet)translator.GetObject(L, 2, typeof(GameFramework.Network.Packet));
                    System.IO.Stream _destination = (System.IO.Stream)translator.GetObject(L, 3, typeof(System.IO.Stream));
                    
                        bool gen_ret = gen_to_be_invoked.Serialize( _packet, _destination );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Shutdown(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaFramework.NetworkHelper gen_to_be_invoked = (LuaFramework.NetworkHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Shutdown(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PacketHeaderLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaFramework.NetworkHelper gen_to_be_invoked = (LuaFramework.NetworkHelper)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.PacketHeaderLength);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}

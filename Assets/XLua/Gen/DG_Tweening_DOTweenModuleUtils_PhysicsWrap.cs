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
    public class DGTweeningDOTweenModuleUtilsPhysicsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DG.Tweening.DOTweenModuleUtils.Physics);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 5, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SetOrientationOnPath", _m_SetOrientationOnPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "HasRigidbody2D", _m_HasRigidbody2D_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "HasRigidbody", _m_HasRigidbody_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateDOTweenPathTween", _m_CreateDOTweenPathTween_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "DG.Tweening.DOTweenModuleUtils.Physics does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetOrientationOnPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Plugins.Options.PathOptions _options;translator.Get(L, 1, out _options);
                    DG.Tweening.Tween _t = (DG.Tweening.Tween)translator.GetObject(L, 2, typeof(DG.Tweening.Tween));
                    UnityEngine.Quaternion _newRot;translator.Get(L, 3, out _newRot);
                    UnityEngine.Transform _trans = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    
                    DG.Tweening.DOTweenModuleUtils.Physics.SetOrientationOnPath( _options, _t, _newRot, _trans );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasRigidbody2D_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Component _target = (UnityEngine.Component)translator.GetObject(L, 1, typeof(UnityEngine.Component));
                    
                        bool gen_ret = DG.Tweening.DOTweenModuleUtils.Physics.HasRigidbody2D( _target );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasRigidbody_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Component _target = (UnityEngine.Component)translator.GetObject(L, 1, typeof(UnityEngine.Component));
                    
                        bool gen_ret = DG.Tweening.DOTweenModuleUtils.Physics.HasRigidbody( _target );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateDOTweenPathTween_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.MonoBehaviour _target = (UnityEngine.MonoBehaviour)translator.GetObject(L, 1, typeof(UnityEngine.MonoBehaviour));
                    bool _tweenRigidbody = LuaAPI.lua_toboolean(L, 2);
                    bool _isLocal = LuaAPI.lua_toboolean(L, 3);
                    DG.Tweening.Plugins.Core.PathCore.Path _path = (DG.Tweening.Plugins.Core.PathCore.Path)translator.GetObject(L, 4, typeof(DG.Tweening.Plugins.Core.PathCore.Path));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 6, out _pathMode);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> gen_ret = DG.Tweening.DOTweenModuleUtils.Physics.CreateDOTweenPathTween( _target, _tweenRigidbody, _isLocal, _path, _duration, _pathMode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}

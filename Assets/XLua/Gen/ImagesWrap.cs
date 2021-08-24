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
    public class ImagesWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Images);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 29, 29);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "clearImage", _g_get_clearImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "collapseImage", _g_get_collapseImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "clearOnNewSceneImage", _g_get_clearOnNewSceneImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "showTimeImage", _g_get_showTimeImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "showSceneImage", _g_get_showSceneImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "userImage", _g_get_userImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "showMemoryImage", _g_get_showMemoryImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "softwareImage", _g_get_softwareImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "dateImage", _g_get_dateImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "showFpsImage", _g_get_showFpsImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "infoImage", _g_get_infoImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "saveLogsImage", _g_get_saveLogsImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "searchImage", _g_get_searchImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "copyImage", _g_get_copyImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "copyAllImage", _g_get_copyAllImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "closeImage", _g_get_closeImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "buildFromImage", _g_get_buildFromImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "systemInfoImage", _g_get_systemInfoImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "graphicsInfoImage", _g_get_graphicsInfoImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "backImage", _g_get_backImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "logImage", _g_get_logImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "warningImage", _g_get_warningImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "errorImage", _g_get_errorImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "barImage", _g_get_barImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "button_activeImage", _g_get_button_activeImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "even_logImage", _g_get_even_logImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "odd_logImage", _g_get_odd_logImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selectedImage", _g_get_selectedImage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reporterScrollerSkin", _g_get_reporterScrollerSkin);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "clearImage", _s_set_clearImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "collapseImage", _s_set_collapseImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "clearOnNewSceneImage", _s_set_clearOnNewSceneImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "showTimeImage", _s_set_showTimeImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "showSceneImage", _s_set_showSceneImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "userImage", _s_set_userImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "showMemoryImage", _s_set_showMemoryImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "softwareImage", _s_set_softwareImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "dateImage", _s_set_dateImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "showFpsImage", _s_set_showFpsImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "infoImage", _s_set_infoImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "saveLogsImage", _s_set_saveLogsImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "searchImage", _s_set_searchImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "copyImage", _s_set_copyImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "copyAllImage", _s_set_copyAllImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "closeImage", _s_set_closeImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "buildFromImage", _s_set_buildFromImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "systemInfoImage", _s_set_systemInfoImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "graphicsInfoImage", _s_set_graphicsInfoImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "backImage", _s_set_backImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "logImage", _s_set_logImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "warningImage", _s_set_warningImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "errorImage", _s_set_errorImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "barImage", _s_set_barImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "button_activeImage", _s_set_button_activeImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "even_logImage", _s_set_even_logImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "odd_logImage", _s_set_odd_logImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "selectedImage", _s_set_selectedImage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reporterScrollerSkin", _s_set_reporterScrollerSkin);
            
			
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
					
					Images gen_ret = new Images();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Images constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_clearImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.clearImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_collapseImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.collapseImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_clearOnNewSceneImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.clearOnNewSceneImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_showTimeImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.showTimeImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_showSceneImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.showSceneImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_userImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.userImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_showMemoryImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.showMemoryImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_softwareImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.softwareImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dateImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.dateImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_showFpsImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.showFpsImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_infoImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.infoImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_saveLogsImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.saveLogsImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_searchImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.searchImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_copyImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.copyImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_copyAllImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.copyAllImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_closeImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.closeImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_buildFromImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.buildFromImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_systemInfoImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.systemInfoImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_graphicsInfoImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.graphicsInfoImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_backImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.backImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_logImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.logImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_warningImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.warningImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_errorImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.errorImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_barImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.barImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_button_activeImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.button_activeImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_even_logImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.even_logImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_odd_logImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.odd_logImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selectedImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.selectedImage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reporterScrollerSkin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reporterScrollerSkin);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_clearImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.clearImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_collapseImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.collapseImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_clearOnNewSceneImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.clearOnNewSceneImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_showTimeImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.showTimeImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_showSceneImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.showSceneImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_userImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.userImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_showMemoryImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.showMemoryImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_softwareImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.softwareImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dateImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.dateImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_showFpsImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.showFpsImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_infoImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.infoImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_saveLogsImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.saveLogsImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_searchImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.searchImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_copyImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.copyImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_copyAllImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.copyAllImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_closeImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.closeImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_buildFromImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.buildFromImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_systemInfoImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.systemInfoImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_graphicsInfoImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.graphicsInfoImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_backImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.backImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_logImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.logImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_warningImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.warningImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_errorImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.errorImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_barImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.barImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_button_activeImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.button_activeImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_even_logImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.even_logImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_odd_logImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.odd_logImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_selectedImage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.selectedImage = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reporterScrollerSkin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Images gen_to_be_invoked = (Images)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reporterScrollerSkin = (UnityEngine.GUISkin)translator.GetObject(L, 2, typeof(UnityEngine.GUISkin));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}

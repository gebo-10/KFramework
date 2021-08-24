//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace LuaFramework
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry
    {
        public static void Init()
        {
            InitBuiltinComponents();
            InitCustomComponents();
        }

        public static T GetComponent<T>() where T : GameFrameworkComponent
        {
            return (T)UnityGameFramework.Runtime.GameEntry.GetComponent<T>();
        }
    }
}

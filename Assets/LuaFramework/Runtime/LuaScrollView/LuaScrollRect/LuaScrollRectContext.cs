using FancyScrollView;
using System;
using UnityEngine;

namespace LuaFramework
{
    class LuaScrollRectContext : FancyScrollRectContext
    {
        public Action<int, GameObject> update_action;
        public Action<float, float> update_position = (float a,float b)=>{};
    }
}

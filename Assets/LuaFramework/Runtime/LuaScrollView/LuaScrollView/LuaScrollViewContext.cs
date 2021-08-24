using FancyScrollView;
using System;
using UnityEngine;

namespace LuaFramework
{
    class LuaScrollViewContext : FancyScrollRectContext
    {
        public Action<int, GameObject> init= (int index, GameObject item) => { };
        public Action<int, GameObject> update_action;
        public Action<float, float> update_position = (float a,float b)=>{};
    }
}

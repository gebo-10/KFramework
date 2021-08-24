using UnityEngine;
using System.Collections.Generic;
using FancyScrollView;
using System;

namespace LuaFramework
{
    class ScrollView : FancyScrollView<ItemData, LuaScrollViewContext>
    {
        [SerializeField] Scroller scroller = default;
        [SerializeField] GameObject cellPrefab = default;

        protected override GameObject CellPrefab => cellPrefab;

        protected override void Initialize()
        {
            base.Initialize();
            scroller.OnValueChanged(UpdatePosition);
        }

        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
            scroller.SetTotalCount(items.Count);
        }

        public void Bind(int num, Action<int, GameObject> update_action)
        {
            Context.update_action = update_action;
            var items = new List<ItemData>();
            for (var i = 0; i < num; i++)
            {
                items.Add(new ItemData());
            }
            UpdateData(items);
        }

        public void SetInitCallback(Action<int, GameObject> init)
        {
            Context.init = init;
        }
        public void SetPositionCallback(Action<float, float> update_position)
        {
            Context.update_position = update_position;
        }
    }
}

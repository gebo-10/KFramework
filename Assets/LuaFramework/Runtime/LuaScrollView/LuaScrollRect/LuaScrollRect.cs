using System;
using System.Collections.Generic;
using UnityEngine;
using EasingCore;
using FancyScrollView;

namespace LuaFramework
{
    class LuaScrollRect : FancyScrollRect<ItemData, LuaScrollRectContext>
    {
        [SerializeField] float cellSize = 100f;
        [SerializeField] GameObject cellPrefab = default;

        protected override float CellSize => cellSize;
        protected override GameObject CellPrefab => cellPrefab;
        public int DataCount => ItemsSource.Count;

        public float PaddingTop
        {
            get => paddingHead;
            set
            {
                paddingHead = value;
                Relayout();
            }
        }

        public float PaddingBottom
        {
            get => paddingTail;
            set
            {
                paddingTail = value;
                Relayout();
            }
        }

        public float Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                Relayout();
            }
        }

        public void Bind(int num, Action<int,GameObject> update_action)
        {
            Context.update_action = update_action;
            var items = new List<ItemData>();
            for(var i=0; i < num; i++)
            {
                items.Add(new ItemData());
            }
            UpdateContents(items);
        }
        public void SetPositionCallback(Action<float, float> update_position)
        {
            Context.update_position = update_position;
        }

        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
        }

        public void ScrollTo(int index, float duration, Ease easing, Alignment alignment = Alignment.Middle)
        {
            ScrollTo(index, duration, easing, GetAlignment(alignment));
        }

        public void JumpTo(int index, Alignment alignment = Alignment.Middle)
        {
            JumpTo(index, GetAlignment(alignment));
        }

        float GetAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.Upper: return 0.0f;
                case Alignment.Middle: return 0.5f;
                case Alignment.Lower: return 1.0f;
                default: return GetAlignment(Alignment.Middle);
            }
        }
    }
}

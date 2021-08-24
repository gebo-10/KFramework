using System;
using UnityEngine;
using EasingCore;
using FancyScrollView;
using System.Collections.Generic;

namespace LuaFramework
{
    class LuaGridView : FancyGridView<ItemData, LuaGridViewContext>
    {
        class CellGroup : DefaultCellGroup { }

        [SerializeField] LuaGridViewCell cellPrefab = default;

        protected override void SetupCellTemplate() => Setup<CellGroup>(cellPrefab);

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

        public float SpacingY
        {
            get => spacing;
            set
            {
                spacing = value;
                Relayout();
            }
        }

        public float SpacingX
        {
            get => startAxisSpacing;
            set
            {
                startAxisSpacing = value;
                Relayout();
            }
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

        public void Bind(int num, Action<int, GameObject> update_action)
        {
            Context.update_action = update_action;
            var items = new List<ItemData>();
            for (var i = 0; i < num; i++)
            {
                items.Add(new ItemData());
            }
            UpdateContents(items);
        }
        public void SetPositionCallback(Action<float, float> update_position)
        {
            Context.update_position = update_position;
        }
    }
}

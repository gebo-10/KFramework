using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace LuaFramework
{
    class LuaScrollRectCell : FancyScrollRectCell<ItemData, LuaScrollRectContext>
    {
        public override void Initialize()
        {
        }

        public override void UpdateContent(ItemData itemData)
        {
            Context.update_action(Index, gameObject);
        }

        protected override void UpdatePosition(float normalizedPosition, float localPosition)
        {
            base.UpdatePosition(normalizedPosition, localPosition);
            Context.update_position(normalizedPosition, localPosition);
        }
    }
}

using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace LuaFramework
{
    class LuaScrollViewCell : FancyCell<ItemData, LuaScrollViewContext>
    {

        public override void Initialize()
        {
            Context.init(Index, gameObject);
        }

        public override void UpdateContent(ItemData itemData)
        {
            Context.update_action(Index, gameObject);
        }

        public override void UpdatePosition(float position)
        {
            currentPosition = position;
            Context.update_position(position, 0);
        }

        // GameObject が非アクティブになると Animator がリセットされてしまうため
        // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
        float currentPosition = 0;

        void OnEnable() => UpdatePosition(currentPosition);
    }
}

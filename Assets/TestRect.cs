using LuaFramework;
using UnityEngine;

namespace LuaFramework
{
    class TestRect:MonoBehaviour
    {
        private void Start()
        {
            var comp = GetComponent<LuaGridView>();
            comp.Bind(100, (int index, GameObject go) => {
                Debug.Log(index);
            });
        }
    }
}

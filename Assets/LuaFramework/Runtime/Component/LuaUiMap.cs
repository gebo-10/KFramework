using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

[Serializable]
public enum UiType
{
    Control,
    Button,
    Text,
    Image,
    DScrollView,
    ScrollView,
    InputField,
    Slider,
    Toggle,
    ToggleGroup,
    Dropdown,
    Tab,

}

[Serializable]
public class UiItem
{
    public string name = "";
    public UiType type;
    public GameObject obj;
    public string type_name;
    public Component component;
    public Component rect;
}

public class LuaUiMap : MonoBehaviour
{
    public List<UiItem> uimap=new List<UiItem>();
    static Dictionary<string, UiType> dic = new Dictionary<string, UiType> {
        { "control", UiType.Control },
        { "btn", UiType.Button },
        { "txt", UiType.Text },
        { "img", UiType.Image },
        { "dropdown", UiType.Dropdown },
        { "sc", UiType.DScrollView },
        { "input", UiType.InputField },
        { "slider", UiType.Slider },
        { "toggle", UiType.Toggle },
        { "group", UiType.ToggleGroup },
        { "tab" , UiType.Tab },
    };
    static Dictionary<string, UiType> compToUiType = new Dictionary<string, UiType> {
        { "DynamicScrollView", UiType.DScrollView },
        { "ScrollRect", UiType.ScrollView },
        { "Slider", UiType.Slider },
        { "Dropdown", UiType.Dropdown },
        { "InputField", UiType.InputField },
        { "ToggleGroup", UiType.ToggleGroup },
        { "Toggle", UiType.Toggle },
        { "Button", UiType.Button },
        { "Text", UiType.Text },
        { "Image", UiType.Image },
    };

    public UiItem[] GetUiList()
    {
        return uimap.ToArray();
    }

    [EditorButton]
    public void UpdateUiMap()
    {
        uimap.Clear();
        ProcessTree(transform);
    }

    private void ProcessTree(Transform root)
    {
        for(int i = 0; i < root.childCount; i++)
        {
            Transform t = root.GetChild(i);
            ProcessNode(t);
            if(t.GetComponent<LuaUiMap>() == null)
            {
                ProcessTree(t);
            }
        }
    }

    private void ProcessNode(Transform t)
    {
        string name = t.name;
        Debug.Log(name);
        string[] arr = name.Split('_');
        if (arr.Length == 0) return;
        
        if (!dic.ContainsKey(arr[0])) return;
        UiItem item = new UiItem();
        UiType type = dic[arr[0]];
        item.type = type;
        item.name = name;
        item.obj = t.gameObject;
        item.type_name = type.ToString();
        item.rect = t.GetComponent<RectTransform>();

        item.component = t.GetComponent(item.type_name);

        if(type== UiType.Text && item.component ==null)
        {
            item.component = t.GetComponent("TextMeshProUGUI");
        }

        if (type == UiType.InputField && item.component == null)
        {
            item.component = t.GetComponent("TMP_InputField");
        }

        if (type == UiType.DScrollView)
        {
            item.component = t.GetComponent("DynamicScrollView");
        }

        //if (type==UiType.Control)
        //{
        //    item.component = t.GetComponent("RectTransform");
        //}
        if (item.type != UiType.Control && item.component == null)
        {
            Debug.LogError("Control type not right name=" + item.name);
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("Error", "Control type not right name=" + item.name, "ok");
#endif
            return;
        }
        uimap.Add(item);
    }
}

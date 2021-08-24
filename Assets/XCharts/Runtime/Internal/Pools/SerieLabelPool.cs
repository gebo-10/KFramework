/******************************************/
/*                                        */
/*     Copyright (c) 2018 monitor1394     */
/*     https://github.com/monitor1394     */
/*                                        */
/******************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XCharts
{
    internal static class SerieLabelPool
    {
        private static readonly Stack<GameObject> m_Stack = new Stack<GameObject>(200);
        private static Dictionary<int, bool> m_ReleaseDic = new Dictionary<int, bool>(1000);

        public static GameObject Get(string name, Transform parent, SerieLabel label, Font font, Color color,
            float iconWidth, float iconHeight)
        {
            GameObject element;
            if (m_Stack.Count == 0 || !Application.isPlaying)
            {
                element = CreateSerieLabel(name, parent, label, font, color, iconWidth, iconHeight);
            }
            else
            {
                element = m_Stack.Pop();
                if (element == null)
                {
                    element = CreateSerieLabel(name, parent, label, font, color, iconWidth, iconHeight);
                }
                m_ReleaseDic.Remove(element.GetInstanceID());
                element.name = name;
                element.transform.SetParent(parent);
                element.transform.localEulerAngles = new Vector3(0, 0, label.rotate);
                var text = element.GetComponentInChildren<Text>();
                text.color = color;
                text.font = font;
                text.fontSize = label.fontSize;
                text.fontStyle = label.fontStyle;
                ChartHelper.SetActive(element, true);
            }
            return element;
        }

        private static GameObject CreateSerieLabel(string name, Transform parent, SerieLabel label, Font font, Color color,
            float iconWidth, float iconHeight)
        {
            var element = ChartHelper.AddSerieLabel(name, parent, font,
                         color, label.backgroundColor, label.fontSize, label.fontStyle, label.rotate,
                         label.backgroundWidth, label.backgroundHeight, 1);
            ChartHelper.AddIcon("Icon", element.transform, iconWidth, iconHeight);
            return element;
        }

        public static void Release(GameObject element)
        {
            if (element == null) return;
            ChartHelper.SetActive(element, false);
            if (!Application.isPlaying) return;
            if (!m_ReleaseDic.ContainsKey(element.GetInstanceID()))
            {
                m_Stack.Push(element);
                m_ReleaseDic.Add(element.GetInstanceID(), true);
            }
        }

        public static void ReleaseAll(Transform parent)
        {
            int count = parent.childCount;
            for (int i = 0; i < count; i++)
            {
                Release(parent.GetChild(i).gameObject);
            }
        }

        public static void ClearAll()
        {
            m_Stack.Clear();
            m_ReleaseDic.Clear();
        }
    }
}

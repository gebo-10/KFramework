/******************************************/
/*                                        */
/*     Copyright (c) 2018 monitor1394     */
/*     https://github.com/monitor1394     */
/*                                        */
/******************************************/

using UnityEditor;
using UnityEngine;

namespace XCharts
{
    /// <summary>
    /// Editor class used to edit UI XChartsMgr.
    /// </summary>

    [CustomEditor(typeof(XChartsMgr), false)]
    public class XChartsMgrEditor : Editor
    {
        protected XChartsMgr m_Target;
        protected SerializedProperty m_Script;
        protected SerializedProperty m_NowVersion;
        protected SerializedProperty m_NewVersion;

        protected virtual void OnEnable()
        {
            m_Target = (XChartsMgr)target;
            m_Script = serializedObject.FindProperty("m_Script");
            m_NowVersion = serializedObject.FindProperty("m_NowVersion");
            m_NewVersion = serializedObject.FindProperty("m_NewVersion");
        }

        public override void OnInspectorGUI()
        {
            if (m_Target == null && target == null)
            {
                base.OnInspectorGUI();
                return;
            }
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_NowVersion);
            EditorGUILayout.PropertyField(m_NewVersion);
            if (GUILayout.Button("检测更新"))
            {
                CheckVersionEditor.ShowWindow();
            }
            if (GUILayout.Button("去Github主页"))
            {
                Application.OpenURL("https://github.com/monitor1394/unity-ugui-XCharts");
            }
            if (GUILayout.Button("点Star支持"))
            {
                Application.OpenURL("https://github.com/monitor1394/unity-ugui-XCharts/stargazers");
            }
            if (GUILayout.Button("问题反馈"))
            {
                Application.OpenURL("https://github.com/monitor1394/unity-ugui-XCharts/issues");
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
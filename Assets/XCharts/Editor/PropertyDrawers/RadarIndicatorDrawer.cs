﻿/******************************************/
/*                                        */
/*     Copyright (c) 2018 monitor1394     */
/*     https://github.com/monitor1394     */
/*                                        */
/******************************************/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XCharts
{
    [CustomPropertyDrawer(typeof(Radar.Indicator), true)]
    public class RadarIndicatorDrawer : PropertyDrawer
    {
        private Dictionary<string, bool> m_RadarModuleToggle = new Dictionary<string, bool>();

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            SerializedProperty m_Name = prop.FindPropertyRelative("m_Name");
            SerializedProperty m_Max = prop.FindPropertyRelative("m_Max");
            SerializedProperty m_Min = prop.FindPropertyRelative("m_Min");
            SerializedProperty m_TextStyle = prop.FindPropertyRelative("m_TextStyle");
            Rect drawRect = pos;
            float defaultLabelWidth = EditorGUIUtility.labelWidth;
            float defaultFieldWidth = EditorGUIUtility.fieldWidth;
            drawRect.height = EditorGUIUtility.singleLineHeight;

            int index = ChartEditorHelper.GetIndexFromPath(prop);
            ChartEditorHelper.MakeFoldout(ref drawRect, ref m_RadarModuleToggle, prop, "Indicator " + index, m_Name, false);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            if (ChartEditorHelper.IsToggle(m_RadarModuleToggle, prop))
            {
                ++EditorGUI.indentLevel;

                EditorGUI.PropertyField(drawRect, m_Name);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_Min);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_Max);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_TextStyle);
                drawRect.y += EditorGUI.GetPropertyHeight(m_TextStyle);

                --EditorGUI.indentLevel;
            }
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            if (ChartEditorHelper.IsToggle(m_RadarModuleToggle, prop))
            {
                var height = 4 * EditorGUIUtility.singleLineHeight + 3 * EditorGUIUtility.standardVerticalSpacing;
                height += EditorGUI.GetPropertyHeight(prop.FindPropertyRelative("m_TextStyle"));
                return height;
            }
            else
            {
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }
    }
}
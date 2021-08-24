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
    [CustomPropertyDrawer(typeof(SerieLabel), true)]
    public class SerieLabelDrawer : PropertyDrawer
    {
        private Dictionary<string, bool> m_SerieLabelToggle = new Dictionary<string, bool>();

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            Rect drawRect = pos;
            drawRect.height = EditorGUIUtility.singleLineHeight;
            SerializedProperty show = prop.FindPropertyRelative("m_Show");
            SerializedProperty m_Position = prop.FindPropertyRelative("m_Position");
            SerializedProperty m_Formatter = prop.FindPropertyRelative("m_Formatter");
            SerializedProperty m_Offset = prop.FindPropertyRelative("m_Offset");
            SerializedProperty m_Margin = prop.FindPropertyRelative("m_Margin");
            SerializedProperty m_Rotate = prop.FindPropertyRelative("m_Rotate");
            SerializedProperty m_PaddingLeftRight = prop.FindPropertyRelative("m_PaddingLeftRight");
            SerializedProperty m_PaddingTopBottom = prop.FindPropertyRelative("m_PaddingTopBottom");
            SerializedProperty m_Color = prop.FindPropertyRelative("m_Color");
            SerializedProperty m_BackgroundColor = prop.FindPropertyRelative("m_BackgroundColor");
            SerializedProperty m_BackgroundWidth = prop.FindPropertyRelative("m_BackgroundWidth");
            SerializedProperty m_BackgroundHeight = prop.FindPropertyRelative("m_BackgroundHeight");
            SerializedProperty m_FontSize = prop.FindPropertyRelative("m_FontSize");
            SerializedProperty m_FontStyle = prop.FindPropertyRelative("m_FontStyle");
            SerializedProperty m_Line = prop.FindPropertyRelative("m_Line");
            SerializedProperty m_LineType = prop.FindPropertyRelative("m_LineType");
            SerializedProperty m_LineColor = prop.FindPropertyRelative("m_LineColor");
            SerializedProperty m_LineWidth = prop.FindPropertyRelative("m_LineWidth");
            SerializedProperty m_LineLength1 = prop.FindPropertyRelative("m_LineLength1");
            SerializedProperty m_LineLength2 = prop.FindPropertyRelative("m_LineLength2");
            SerializedProperty m_Border = prop.FindPropertyRelative("m_Border");
            SerializedProperty m_BorderWidth = prop.FindPropertyRelative("m_BorderWidth");
            SerializedProperty m_BorderColor = prop.FindPropertyRelative("m_BorderColor");
            SerializedProperty m_NumericFormatter = prop.FindPropertyRelative("m_NumericFormatter");
            SerializedProperty m_AutoOffset = prop.FindPropertyRelative("m_AutoOffset");

            ChartEditorHelper.MakeFoldout(ref drawRect, ref m_SerieLabelToggle, prop, null, show, false);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            if (ChartEditorHelper.IsToggle(m_SerieLabelToggle, prop))
            {
                ++EditorGUI.indentLevel;
                EditorGUI.PropertyField(drawRect, m_Position);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                EditorGUI.PropertyField(drawRect, m_Offset);
                drawRect.y += EditorGUI.GetPropertyHeight(m_Offset);

                EditorGUI.PropertyField(drawRect, m_AutoOffset);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_Margin);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_Formatter);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_NumericFormatter);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_Color);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_BackgroundColor);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_BackgroundWidth);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_BackgroundHeight);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_Rotate);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_PaddingLeftRight);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_PaddingTopBottom);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_FontSize);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_FontStyle);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_Border);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_BorderWidth);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_BorderColor);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_Line);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_LineType);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_LineColor);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_LineWidth);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_LineLength1);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_LineLength2);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                --EditorGUI.indentLevel;
            }
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            float height = 0;
            if (ChartEditorHelper.IsToggle(m_SerieLabelToggle, prop))
            {
                height += 24 * EditorGUIUtility.singleLineHeight + 23 * EditorGUIUtility.standardVerticalSpacing;
                height += EditorGUI.GetPropertyHeight(prop.FindPropertyRelative("m_Offset"));
            }
            else
            {
                height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
            return height;
        }
    }
}
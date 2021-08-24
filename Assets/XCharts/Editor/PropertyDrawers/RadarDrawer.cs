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
    [CustomPropertyDrawer(typeof(Radar), true)]
    public class RadarDrawer : PropertyDrawer
    {
        SerializedProperty m_Shape;
        SerializedProperty m_PositionType;
        SerializedProperty m_Radius;
        SerializedProperty m_SplitNumber;
        SerializedProperty m_Center;
        SerializedProperty m_SplitLine;
        SerializedProperty m_SplitArea;
        SerializedProperty m_Indicator;
        SerializedProperty m_IndicatorGap;
        SerializedProperty m_CeilRate;
        SerializedProperty m_IndicatorList;
        SerializedProperty m_IsAxisTooltip;

        private Dictionary<string, bool> m_RadarModuleToggle = new Dictionary<string, bool>();
        private Dictionary<string, bool> m_IndicatorToggle = new Dictionary<string, bool>();
        private bool m_IndicatorJsonAreaToggle = false;
        private string m_IndicatorJsonAreaText;
        private int m_IndicatorSize;
        private int m_BackgroundColorSize;

        private void InitProperty(SerializedProperty prop)
        {
            m_Shape = prop.FindPropertyRelative("m_Shape");
            m_PositionType = prop.FindPropertyRelative("m_PositionType");
            m_Radius = prop.FindPropertyRelative("m_Radius");
            m_SplitNumber = prop.FindPropertyRelative("m_SplitNumber");
            m_Center = prop.FindPropertyRelative("m_Center");
            m_SplitLine = prop.FindPropertyRelative("m_SplitLine");
            m_SplitArea = prop.FindPropertyRelative("m_SplitArea");
            m_Indicator = prop.FindPropertyRelative("m_Indicator");
            m_IndicatorGap = prop.FindPropertyRelative("m_IndicatorGap");
            m_CeilRate = prop.FindPropertyRelative("m_CeilRate");
            m_IndicatorList = prop.FindPropertyRelative("m_IndicatorList");
            m_IsAxisTooltip = prop.FindPropertyRelative("m_IsAxisTooltip");
        }

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            InitProperty(prop);
            Rect drawRect = pos;
            float defaultLabelWidth = EditorGUIUtility.labelWidth;
            float defaultFieldWidth = EditorGUIUtility.fieldWidth;
            drawRect.height = EditorGUIUtility.singleLineHeight;
            int index = ChartEditorHelper.GetIndexFromPath(prop);
            ChartEditorHelper.MakeFoldout(ref drawRect, ref m_RadarModuleToggle, prop, "Radar " + index, null, true);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            if (ChartEditorHelper.IsToggle(m_RadarModuleToggle, prop))
            {
                ++EditorGUI.indentLevel;

                EditorGUIUtility.labelWidth = defaultLabelWidth;
                EditorGUIUtility.fieldWidth = defaultFieldWidth;
                EditorGUI.PropertyField(drawRect, m_Shape);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_PositionType);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                EditorGUI.LabelField(drawRect, "Center");
                var startX = drawRect.x + EditorGUIUtility.labelWidth - EditorGUI.indentLevel * 15;
                var tempWidth = (pos.width - startX + 35) / 2;
                var centerXRect = new Rect(startX, drawRect.y, tempWidth, drawRect.height);
                var centerYRect = new Rect(centerXRect.x + tempWidth - 20, drawRect.y, tempWidth, drawRect.height);
                while (m_Center.arraySize < 2) m_Center.arraySize++;
                EditorGUI.PropertyField(centerXRect, m_Center.GetArrayElementAtIndex(0), GUIContent.none);
                EditorGUI.PropertyField(centerYRect, m_Center.GetArrayElementAtIndex(1), GUIContent.none);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                EditorGUI.PropertyField(drawRect, m_Radius);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_SplitNumber);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_CeilRate);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(drawRect, m_IsAxisTooltip);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                EditorGUI.PropertyField(drawRect, m_SplitLine);
                drawRect.y += EditorGUI.GetPropertyHeight(m_SplitLine);
                EditorGUI.PropertyField(drawRect, m_SplitArea);
                drawRect.y += EditorGUI.GetPropertyHeight(m_SplitArea);
                drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                ChartEditorHelper.MakeFoldout(ref drawRect, ref m_IndicatorToggle, prop, "Indicators", m_Indicator, false);
                ChartEditorHelper.MakeJsonData(ref drawRect, ref m_IndicatorJsonAreaToggle, ref m_IndicatorJsonAreaText, prop, pos.width, 20);
                drawRect.width = pos.width;
                drawRect.x = pos.x;
                if (ChartEditorHelper.IsToggle(m_IndicatorToggle, prop))
                {
                    EditorGUI.PropertyField(drawRect, m_IndicatorGap);
                    drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    ChartEditorHelper.MakeList(ref drawRect, ref m_IndicatorSize, m_IndicatorList);
                }
                --EditorGUI.indentLevel;
            }
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            int propNum = 1;
            if (ChartEditorHelper.IsToggle(m_RadarModuleToggle, prop))
            {
                propNum += 9;
                if (m_IndicatorJsonAreaToggle) propNum += 4;
                float height = propNum * EditorGUIUtility.singleLineHeight + (propNum - 1) * EditorGUIUtility.standardVerticalSpacing;
                height += EditorGUI.GetPropertyHeight(prop.FindPropertyRelative("m_SplitLine"));
                height += EditorGUI.GetPropertyHeight(prop.FindPropertyRelative("m_SplitArea"));

                if (ChartEditorHelper.IsToggle(m_IndicatorToggle, prop))
                {
                    m_IndicatorList = prop.FindPropertyRelative("m_IndicatorList");
                    height += EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing;

                    for (int i = 0; i < m_IndicatorList.arraySize; i++)
                    {
                        height += EditorGUI.GetPropertyHeight(m_IndicatorList.GetArrayElementAtIndex(i));
                    }
                }
                return height;
            }
            else
            {
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }
    }
}
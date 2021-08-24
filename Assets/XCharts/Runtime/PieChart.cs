﻿/******************************************/
/*                                        */
/*     Copyright (c) 2018 monitor1394     */
/*     https://github.com/monitor1394     */
/*                                        */
/******************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XCharts
{
    [AddComponentMenu("XCharts/PieChart", 15)]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public partial class PieChart : BaseChart
    {
        private bool isDrawPie;
        private bool m_IsEnterLegendButtom;

        protected Action<PointerEventData, int, int> m_OnPointerClickPie;

        protected override void Awake()
        {
            base.Awake();
            raycastTarget = false;
        }

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();
            m_Title.text = "PieChart";
            m_Legend.show = true;
            RemoveData();
            AddSerie(SerieType.Pie, "serie1");
            AddData(0, 70, "pie1");
            AddData(0, 20, "pie2");
            AddData(0, 10, "pie3");
        }
#endif

        protected override void Update()
        {
            base.Update();
            if (!isDrawPie) RefreshChart();
        }

        protected override void DrawChart(VertexHelper vh)
        {
            base.DrawChart(vh);
            UpdateRuntimeData();
            DrawLabelLine(vh);
            DrawPie(vh);
            DrawLabelBackground(vh);
        }

        private void UpdateRuntimeData()
        {
            for (int i = 0; i < m_Series.Count; i++)
            {
                var serie = m_Series.list[i];
                if (serie.type != SerieType.Pie) continue;
                serie.index = i;
                var data = serie.data;
                serie.runtimeDataMax = serie.yMax;
                serie.runtimePieDataTotal = serie.yTotal;
                serie.animation.InitProgress(data.Count, 0, 360);
                SerieHelper.UpdateCenter(serie, chartPosition, chartWidth, chartHeight);
                float totalDegree = 360f;
                float startDegree = 0;
                int showdataCount = 0;
                foreach (var sd in serie.data)
                {
                    if (sd.show && serie.pieRoseType == RoseType.Area) showdataCount++;
                    sd.canShowLabel = false;
                }
                float dataChangeDuration = serie.animation.GetUpdateAnimationDuration();
                bool isAllZeroValue = SerieHelper.IsAllZeroValue(serie, 1);
                float zeroReplaceValue = totalDegree / data.Count;
                if (isAllZeroValue)
                {
                    serie.runtimeDataMax = zeroReplaceValue;
                    serie.runtimePieDataTotal = totalDegree;
                }
                for (int n = 0; n < data.Count; n++)
                {
                    var serieData = data[n];
                    serieData.index = n;
                    float value = isAllZeroValue ? zeroReplaceValue : serieData.GetCurrData(1, dataChangeDuration);
                    serieData.runtimePieStartAngle = startDegree;
                    serieData.runtimePieToAngle = startDegree;
                    serieData.runtimePieHalfAngle = startDegree;
                    serieData.runtimePieCurrAngle = startDegree;
                    if (!serieData.show)
                    {
                        continue;
                    }
                    float degree = serie.pieRoseType == RoseType.Area ?
                        (totalDegree / showdataCount) : (totalDegree * value / serie.runtimePieDataTotal);
                    serieData.runtimePieToAngle = startDegree + degree;

                    serieData.runtimePieOutsideRadius = serie.pieRoseType > 0 ?
                        serie.runtimeInsideRadius + (serie.runtimeOutsideRadius - serie.runtimeInsideRadius) * value / serie.runtimeDataMax :
                        serie.runtimeOutsideRadius;
                    if (serieData.highlighted)
                    {
                        serieData.runtimePieOutsideRadius += m_Settings.pieTooltipExtraRadius;
                    }
                    var offset = 0f;
                    if (serie.pieClickOffset && serieData.selected)
                    {
                        offset += m_Settings.pieSelectedOffset;
                    }
                    if (serie.animation.CheckDetailBreak(serieData.runtimePieToAngle))
                    {
                        serieData.runtimePieCurrAngle = serie.animation.GetCurrDetail();
                    }
                    else
                    {
                        serieData.runtimePieCurrAngle = serieData.runtimePieToAngle;
                    }
                    var halfDegree = (serieData.runtimePieToAngle - startDegree) / 2;
                    serieData.runtimePieHalfAngle = startDegree + halfDegree;
                    serieData.runtiemPieOffsetCenter = serie.runtimeCenterPos;
                    serieData.runtimePieInsideRadius = serie.runtimeInsideRadius;
                    if (offset > 0)
                    {
                        var currRad = serieData.runtimePieHalfAngle * Mathf.Deg2Rad;
                        var currSin = Mathf.Sin(currRad);
                        var currCos = Mathf.Cos(currRad);
                        serieData.runtimePieOffsetRadius = 0;
                        serieData.runtimePieInsideRadius -= serieData.runtimePieOffsetRadius;
                        serieData.runtimePieOutsideRadius -= serieData.runtimePieOffsetRadius;
                        if (serie.pieClickOffset && serieData.selected)
                        {
                            serieData.runtimePieOffsetRadius += m_Settings.pieSelectedOffset;
                            if (serieData.runtimePieInsideRadius > 0) serieData.runtimePieInsideRadius += m_Settings.pieSelectedOffset;
                            serieData.runtimePieOutsideRadius += m_Settings.pieSelectedOffset;
                        }
                        serieData.runtiemPieOffsetCenter = new Vector3(serie.runtimeCenterPos.x + serieData.runtimePieOffsetRadius * currSin,
                            serie.runtimeCenterPos.y + serieData.runtimePieOffsetRadius * currCos);
                    }
                    serieData.canShowLabel = serieData.runtimePieCurrAngle >= serieData.runtimePieHalfAngle;
                    startDegree = serieData.runtimePieToAngle;
                    SerieLabelHelper.UpdatePieLabelPosition(serie, serieData);
                }
                SerieLabelHelper.AvoidLabelOverlap(serie);
            }
        }

        private void DrawCenter(VertexHelper vh, Serie serie, ItemStyle itemStyle, float insideRadius)
        {
            if (!ChartHelper.IsClearColor(itemStyle.centerColor))
            {
                var radius = insideRadius - itemStyle.centerGap;
                ChartDrawer.DrawCricle(vh, serie.runtimeCenterPos, radius, itemStyle.centerColor, m_Settings.cicleSmoothness);
            }
        }

        private void DrawPie(VertexHelper vh)
        {
            bool isClickOffset = false;
            bool isDataHighlight = false;
            for (int i = 0; i < m_Series.Count; i++)
            {
                var serie = m_Series.list[i];
                serie.index = i;
                var data = serie.data;
                serie.animation.InitProgress(data.Count, 0, 360);
                if (!serie.show || serie.animation.HasFadeOut())
                {
                    continue;
                }
                if (serie.pieClickOffset) isClickOffset = true;
                bool dataChanging = false;
                for (int n = 0; n < data.Count; n++)
                {
                    var serieData = data[n];
                    if (!serieData.show)
                    {
                        continue;
                    }
                    var itemStyle = SerieHelper.GetItemStyle(serie, serieData, serieData.highlighted);
                    if (serieData.IsDataChanged()) dataChanging = true;
                    var serieNameCount = m_LegendRealShowName.IndexOf(serieData.legendName);
                    var color = SerieHelper.GetItemColor(serie, serieData, m_ThemeInfo, serieNameCount, serieData.highlighted);
                    var toColor = SerieHelper.GetItemToColor(serie, serieData, m_ThemeInfo, serieNameCount, serieData.highlighted);
                    var borderWidth = itemStyle.borderWidth;
                    var borderColor = itemStyle.borderColor;

                    if (serieData.highlighted)
                    {
                        isDataHighlight = true;
                    }
                    if (serie.pieClickOffset && serieData.selected)
                    {
                        var drawEndDegree = serieData.runtimePieCurrAngle;
                        var needRoundCap = serie.roundCap && serieData.runtimePieInsideRadius > 0;
                        ChartDrawer.DrawDoughnut(vh, serieData.runtiemPieOffsetCenter, serieData.runtimePieInsideRadius,
                            serieData.runtimePieOutsideRadius, color, toColor, Color.clear, serieData.runtimePieStartAngle, drawEndDegree,
                             borderWidth, borderColor, serie.pieSpace / 2, m_Settings.cicleSmoothness, needRoundCap, true);
                    }
                    else
                    {
                        var drawEndDegree = serieData.runtimePieCurrAngle;
                        var needRoundCap = serie.roundCap && serieData.runtimePieInsideRadius > 0;
                        ChartDrawer.DrawDoughnut(vh, serie.runtimeCenterPos, serieData.runtimePieInsideRadius, serieData.runtimePieOutsideRadius,
                            color, toColor, Color.clear, serieData.runtimePieStartAngle, drawEndDegree, borderWidth, borderColor, serie.pieSpace / 2,
                            m_Settings.cicleSmoothness, needRoundCap, true);
                        DrawCenter(vh, serie, itemStyle, serieData.runtimePieInsideRadius);
                    }
                    isDrawPie = true;
                    if (!serie.animation.CheckDetailBreak(serieData.runtimePieToAngle)) serie.animation.SetDataFinish(n);
                    else break;
                }
                if (!serie.animation.IsFinish())
                {
                    serie.animation.CheckProgress(360);
                    serie.animation.CheckSymbol(serie.symbol.size);
                    RefreshChart();
                }
                if (dataChanging)
                {
                    RefreshChart();
                }
            }
            raycastTarget = isClickOffset && isDataHighlight;
        }

        private void DrawLabelLine(VertexHelper vh)
        {
            foreach (var serie in m_Series.list)
            {
                if (serie.type != SerieType.Pie) continue;
                foreach (var serieData in serie.data)
                {
                    var serieLabel = SerieHelper.GetSerieLabel(serie, serieData);
                    if (SerieLabelHelper.CanShowLabel(serie, serieData, serieLabel, 1))
                    {
                        int colorIndex = m_LegendRealShowName.IndexOf(serieData.name);
                        Color color = m_ThemeInfo.GetColor(colorIndex);
                        DrawLabelLine(vh, serie, serieData, color);
                    }
                }
            }
        }

        private void DrawLabelBackground(VertexHelper vh)
        {
            foreach (var serie in m_Series.list)
            {
                if (serie.type != SerieType.Pie) continue;
                if (serie.avoidLabelOverlap) continue;
                foreach (var serieData in serie.data)
                {
                    var serieLabel = SerieHelper.GetSerieLabel(serie, serieData);
                    if (SerieLabelHelper.CanShowLabel(serie, serieData, serieLabel, 1))
                    {
                        SerieLabelHelper.UpdatePieLabelPosition(serie, serieData);
                        DrawLabelBackground(vh, serie, serieData);
                    }
                }
            }
        }

        private void DrawLabelLine(VertexHelper vh, Serie serie, SerieData serieData, Color color)
        {
            var serieLabel = SerieHelper.GetSerieLabel(serie, serieData);
            if (serieLabel.show
                && serieLabel.position == SerieLabel.Position.Outside
                && serieLabel.line)
            {
                var insideRadius = serieData.runtimePieInsideRadius;
                var outSideRadius = serieData.runtimePieOutsideRadius;
                var center = serie.runtimeCenterPos;
                var currAngle = serieData.runtimePieHalfAngle;
                if (!ChartHelper.IsClearColor(serieLabel.lineColor)) color = serieLabel.lineColor;
                else if (serieLabel.lineType == SerieLabel.LineType.HorizontalLine) color *= color;
                float currSin = Mathf.Sin(currAngle * Mathf.Deg2Rad);
                float currCos = Mathf.Cos(currAngle * Mathf.Deg2Rad);
                var radius1 = serieLabel.lineType == SerieLabel.LineType.HorizontalLine ?
                    serie.runtimeOutsideRadius : outSideRadius;
                var radius2 = serie.runtimeOutsideRadius + serieLabel.lineLength1;
                var radius3 = insideRadius + (outSideRadius - insideRadius) / 2;
                if (radius1 < serie.runtimeInsideRadius) radius1 = serie.runtimeInsideRadius;
                radius1 -= 0.1f;
                var pos0 = new Vector3(center.x + radius3 * currSin, center.y + radius3 * currCos);
                var pos1 = new Vector3(center.x + radius1 * currSin, center.y + radius1 * currCos);
                var pos2 = serieData.labelPosition;
                if (pos2.x == 0)
                {
                    pos2 = new Vector3(center.x + radius2 * currSin, center.y + radius2 * currCos);
                }
                Vector3 pos4, pos6;
                var horizontalLineCircleRadius = serieLabel.lineWidth * 4f;
                var lineCircleDiff = horizontalLineCircleRadius - 0.3f;
                if (currAngle < 90)
                {
                    var r4 = Mathf.Sqrt(radius1 * radius1 - Mathf.Pow(currCos * radius3, 2)) - currSin * radius3;
                    r4 += serieLabel.lineLength1 - lineCircleDiff;
                    pos6 = pos0 + Vector3.right * lineCircleDiff;
                    pos4 = pos6 + Vector3.right * r4;
                }
                else if (currAngle < 180)
                {
                    var r4 = Mathf.Sqrt(radius1 * radius1 - Mathf.Pow(currCos * radius3, 2)) - currSin * radius3;
                    r4 += serieLabel.lineLength1 - lineCircleDiff;
                    pos6 = pos0 + Vector3.right * lineCircleDiff;
                    pos4 = pos6 + Vector3.right * r4;
                }
                else if (currAngle < 270)
                {
                    var currSin1 = Mathf.Sin((360 - currAngle) * Mathf.Deg2Rad);
                    var currCos1 = Mathf.Cos((360 - currAngle) * Mathf.Deg2Rad);
                    var r4 = Mathf.Sqrt(radius1 * radius1 - Mathf.Pow(currCos1 * radius3, 2)) - currSin1 * radius3;
                    r4 += serieLabel.lineLength1 - lineCircleDiff;
                    pos6 = pos0 + Vector3.left * lineCircleDiff;
                    pos4 = pos6 + Vector3.left * r4;
                }
                else
                {
                    var currSin1 = Mathf.Sin((360 - currAngle) * Mathf.Deg2Rad);
                    var currCos1 = Mathf.Cos((360 - currAngle) * Mathf.Deg2Rad);
                    var r4 = Mathf.Sqrt(radius1 * radius1 - Mathf.Pow(currCos1 * radius3, 2)) - currSin1 * radius3;
                    r4 += serieLabel.lineLength1 - lineCircleDiff;
                    pos6 = pos0 + Vector3.left * lineCircleDiff;
                    pos4 = pos6 + Vector3.left * r4;
                }
                var pos5 = new Vector3(currAngle > 180 ? pos2.x - serieLabel.lineLength2 : pos2.x + serieLabel.lineLength2, pos2.y);
                switch (serieLabel.lineType)
                {
                    case SerieLabel.LineType.BrokenLine:
                        ChartDrawer.DrawLine(vh, pos1, pos2, pos5, serieLabel.lineWidth, color);
                        break;
                    case SerieLabel.LineType.Curves:
                        ChartDrawer.DrawCurves(vh, pos1, pos5, pos1, pos2, serieLabel.lineWidth, color, m_Settings.lineSmoothness);
                        break;
                    case SerieLabel.LineType.HorizontalLine:
                        ChartDrawer.DrawCricle(vh, pos0, horizontalLineCircleRadius, color);
                        ChartDrawer.DrawLine(vh, pos6, pos4, serieLabel.lineWidth, color);
                        break;
                }
            }
        }

        protected override void OnRefreshLabel()
        {
            int serieNameCount = -1;
            for (int i = 0; i < m_Series.Count; i++)
            {
                var serie = m_Series.list[i];
                serie.index = i;
                if (!serie.show) continue;
                var data = serie.data;
                for (int n = 0; n < data.Count; n++)
                {
                    var serieData = data[n];
                    if (!serieData.canShowLabel || serie.IsIgnoreValue(serieData.GetData(1)))
                    {
                        serieData.SetLabelActive(false);
                        continue;
                    }
                    if (!serieData.show) continue;
                    serieNameCount = m_LegendRealShowName.IndexOf(serieData.name);
                    Color color = m_ThemeInfo.GetColor(serieNameCount);
                    DrawLabel(serie, n, serieData, color);
                }
            }
        }

        private void DrawLabel(Serie serie, int dataIndex, SerieData serieData, Color serieColor)
        {
            if (serieData.labelObject == null) return;
            var currAngle = serieData.runtimePieHalfAngle;
            var isHighlight = (serieData.highlighted && serie.emphasis.label.show);
            var serieLabel = SerieHelper.GetSerieLabel(serie, serieData);
            var showLabel = ((serieLabel.show || isHighlight) && serieData.canShowLabel);
            if (showLabel || serieData.iconStyle.show)
            {
                serieData.SetLabelActive(showLabel);
                float rotate = 0;
                bool isInsidePosition = serieLabel.position == SerieLabel.Position.Inside;
                if (serieLabel.rotate > 0 && isInsidePosition)
                {
                    if (currAngle > 180) rotate += 270 - currAngle;
                    else rotate += -(currAngle - 90);
                }
                Color color = serieColor;
                if (isHighlight)
                {
                    if (!ChartHelper.IsClearColor(serie.emphasis.label.color)) color = serie.emphasis.label.color;
                }
                else if (!ChartHelper.IsClearColor(serieLabel.color))
                {
                    color = serieLabel.color;
                }
                else
                {
                    color = isInsidePosition ? Color.white : serieColor;
                }
                var fontSize = isHighlight ? serie.emphasis.label.fontSize : serieLabel.fontSize;
                var fontStyle = isHighlight ? serie.emphasis.label.fontStyle : serieLabel.fontStyle;

                serieData.labelObject.label.color = color;
                serieData.labelObject.label.fontSize = fontSize;
                serieData.labelObject.label.fontStyle = fontStyle;
                serieData.labelObject.SetLabelRotate(rotate);
                if (!string.IsNullOrEmpty(serieLabel.formatter))
                {
                    var value = serieData.data[1];
                    var total = serie.yTotal;
                    var content = SerieLabelHelper.GetFormatterContent(serie, serieData, value, total, serieLabel);
                    if (serieData.labelObject.SetText(content)) RefreshChart();
                }
                else
                {
                    if (serieData.labelObject.SetText(serieData.name)) RefreshChart();
                }
                serieData.labelObject.SetPosition(SerieLabelHelper.GetRealLabelPosition(serieData, serieLabel));
                if (showLabel) serieData.labelObject.SetLabelPosition(serieLabel.offset);
                else serieData.SetLabelActive(false);
            }
            else
            {
                serieData.SetLabelActive(false);
            }
            serieData.labelObject.UpdateIcon(serieData.iconStyle);
        }

        protected override void OnLegendButtonClick(int index, string legendName, bool show)
        {
            LegendHelper.CheckDataShow(m_Series, legendName, show);
            UpdateLegendColor(legendName, show);
            RefreshChart();
        }

        protected override void OnLegendButtonEnter(int index, string legendName)
        {
            m_IsEnterLegendButtom = true;
            LegendHelper.CheckDataHighlighted(m_Series, legendName, true);
            RefreshChart();
        }

        protected override void OnLegendButtonExit(int index, string legendName)
        {
            m_IsEnterLegendButtom = false;
            LegendHelper.CheckDataHighlighted(m_Series, legendName, false);
            RefreshChart();
        }

        protected override void CheckTootipArea(Vector2 local)
        {
            if (m_IsEnterLegendButtom) return;
            m_Tooltip.runtimeDataIndex.Clear();
            bool selected = false;
            foreach (var serie in m_Series.list)
            {
                int index = GetPosPieIndex(serie, local);
                m_Tooltip.runtimeDataIndex.Add(index);
                if (serie.type != SerieType.Pie) continue;
                bool refresh = false;
                for (int j = 0; j < serie.data.Count; j++)
                {
                    var serieData = serie.data[j];
                    if (serieData.highlighted != (j == index)) refresh = true;
                    serieData.highlighted = j == index;
                }
                if (index >= 0) selected = true;
                if (refresh) RefreshChart();
            }
            if (selected)
            {
                m_Tooltip.UpdateContentPos(local + m_Tooltip.offset);
                UpdateTooltip();
            }
            else if (m_Tooltip.IsActive())
            {
                m_Tooltip.SetActive(false);
                RefreshChart();
            }
        }

        private int GetPosPieIndex(Serie serie, Vector2 local)
        {
            if (serie.type != SerieType.Pie) return -1;
            var dist = Vector2.Distance(local, serie.runtimeCenterPos);
            if (dist < serie.runtimeInsideRadius || dist > serie.runtimeOutsideRadius) return -1;
            Vector2 dir = local - new Vector2(serie.runtimeCenterPos.x, serie.runtimeCenterPos.y);
            float angle = ChartHelper.GetAngle360(Vector2.up, dir);
            for (int i = 0; i < serie.data.Count; i++)
            {
                var serieData = serie.data[i];
                if (angle >= serieData.runtimePieStartAngle && angle <= serieData.runtimePieToAngle)
                {
                    return i;
                }
            }
            return -1;
        }

        protected override void UpdateTooltip()
        {
            base.UpdateTooltip();
            bool showTooltip = false;
            foreach (var serie in m_Series.list)
            {
                int index = m_Tooltip.runtimeDataIndex[serie.index];
                if (index < 0) continue;
                showTooltip = true;
                var content = TooltipHelper.GetFormatterContent(m_Tooltip, index, m_Series, m_ThemeInfo);
                TooltipHelper.SetContentAndPosition(tooltip, content, chartRect);
            }
            m_Tooltip.SetActive(showTooltip);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (pointerPos == Vector2.zero) return;
            var refresh = false;
            for (int i = 0; i < m_Series.Count; i++)
            {
                var serie = m_Series.GetSerie(i);
                if (serie.type != SerieType.Pie) continue;
                var index = GetPosPieIndex(serie, pointerPos);
                if (index >= 0)
                {
                    refresh = true;
                    for (int j = 0; j < serie.data.Count; j++)
                    {
                        if (j == index) serie.data[j].selected = !serie.data[j].selected;
                        else serie.data[j].selected = false;
                    }
                    if (m_OnPointerClickPie != null)
                    {
                        m_OnPointerClickPie(eventData, i, index);
                    }
                }
            }
            if (refresh) RefreshChart();
        }
    }
}

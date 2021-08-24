/******************************************/
/*                                        */
/*     Copyright (c) 2018 monitor1394     */
/*     https://github.com/monitor1394     */
/*                                        */
/******************************************/
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace XCharts
{
    public static class AxisHelper
    {
        public static float GetTickWidth(Axis axis)
        {
            return axis.axisTick.width != 0 ? axis.axisTick.width : axis.axisLine.width;
        }

        /// <summary>
        /// 包含箭头偏移的轴线长度
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static float GetAxisLineSymbolOffset(Axis axis)
        {
            if (axis.axisLine.show && axis.axisLine.symbol && axis.axisLine.symbolOffset > 0)
            {
                return axis.axisLine.symbolOffset;
            }
            return 0;
        }

        /// <summary>
        /// 获得分割段数
        /// </summary>
        /// <param name="dataZoom"></param>
        /// <returns></returns>
        public static int GetSplitNumber(Axis axis, float coordinateWid, DataZoom dataZoom)
        {
            if (axis.type == Axis.AxisType.Value)
            {
                if (axis.interval > 0)
                {
                    if (coordinateWid <= 0) return 0;
                    int num = Mathf.CeilToInt(axis.runtimeMinMaxRange / axis.interval);
                    int maxNum = Mathf.CeilToInt(coordinateWid / 15);
                    if (num > maxNum)
                    {
                        axis.interval *= 2;
                        num = Mathf.CeilToInt(axis.runtimeMinMaxRange / axis.interval);
                    }
                    return num;
                }
                else
                {
                    return axis.splitNumber > 0 ? axis.splitNumber : 4;
                }
            }
            else if (axis.type == Axis.AxisType.Log)
            {
                return axis.splitNumber > 0 ? axis.splitNumber : 4;
            }
            else if (axis.type == Axis.AxisType.Category)
            {
                int dataCount = axis.GetDataList(dataZoom).Count;
                if (axis.splitNumber <= 0 || axis.splitNumber > dataCount) return dataCount;
                if (dataCount >= axis.splitNumber * 2) return axis.splitNumber;
                else return dataCount;
            }
            return 0;
        }

        /// <summary>
        /// 获得分割段的宽度
        /// </summary>
        /// <param name="coordinateWidth"></param>
        /// <param name="dataZoom"></param>
        /// <returns></returns>
        public static float GetSplitWidth(Axis axis, float coordinateWidth, DataZoom dataZoom)
        {
            int split = GetSplitNumber(axis, coordinateWidth, dataZoom);
            int segment = (axis.boundaryGap ? split : split - 1);
            segment = segment <= 0 ? 1 : segment;
            return coordinateWidth / segment;
        }

        /// <summary>
        /// 获得一个类目数据在坐标系中代表的宽度
        /// </summary>
        /// <param name="coordinateWidth"></param>
        /// <param name="dataZoom"></param>
        /// <returns></returns>
        public static float GetDataWidth(Axis axis, float coordinateWidth, int dataCount, DataZoom dataZoom)
        {
            if (dataCount < 1) dataCount = 1;
            var categoryCount = axis.GetDataNumber(dataZoom);
            int segment = (axis.boundaryGap ? categoryCount : categoryCount - 1);
            segment = segment <= 0 ? dataCount : segment;
            return coordinateWidth / segment;
        }

        /// <summary>
        /// 获得标签显示的名称
        /// </summary>
        /// <param name="index"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="dataZoom"></param>
        /// <returns></returns>
        internal static string GetLabelName(Axis axis, float coordinateWidth, int index, float minValue, float maxValue,
            DataZoom dataZoom, bool forcePercent)
        {
            int split = GetSplitNumber(axis, coordinateWidth, dataZoom);
            if (axis.type == Axis.AxisType.Value)
            {
                if (minValue == 0 && maxValue == 0) return string.Empty;
                float value = 0;
                if (forcePercent) maxValue = 100;
                if (axis.interval > 0)
                {
                    if (index == split) value = maxValue;
                    else value = minValue + index * axis.interval;
                }
                else
                {
                    value = (minValue + (maxValue - minValue) * index / split);
                    if (!axis.clockwise && value != minValue) value = maxValue - value;
                }
                if (axis.inverse)
                {
                    value = -value;
                    minValue = -minValue;
                    maxValue = -maxValue;
                }
                if (forcePercent) return string.Format("{0}%", (int)value);
                else return axis.axisLabel.GetFormatterContent(value, minValue, maxValue);
            }
            else if (axis.type == Axis.AxisType.Log)
            {
                float value = axis.logBaseE ? Mathf.Exp(axis.runtimeMinLogIndex + index) :
                    Mathf.Pow(axis.logBase, axis.runtimeMinLogIndex + index);
                if (axis.inverse)
                {
                    value = -value;
                    minValue = -minValue;
                    maxValue = -maxValue;
                }
                return axis.axisLabel.GetFormatterContent(value, minValue, maxValue, true);
            }
            var showData = axis.GetDataList(dataZoom);
            int dataCount = showData.Count;
            if (dataCount <= 0) return "";
            int rate = Mathf.RoundToInt(dataCount * 1f / split);
            int newIndex = index * rate;
            if (newIndex <= dataCount - 1)
            {
                return axis.axisLabel.GetFormatterContent(showData[newIndex]);
            }
            else
            {
                if (rate == 1) return string.Empty;
                else if (axis.boundaryGap && coordinateWidth / dataCount > 10) return string.Empty;
                else
                {
                    if ((index - 1) * rate > dataCount - 1) return string.Empty;
                    else return axis.axisLabel.GetFormatterContent(showData[dataCount - 1]);
                }
            }
        }

        /// <summary>
        /// 获得分割线条数
        /// </summary>
        /// <param name="dataZoom"></param>
        /// <returns></returns>
        internal static int GetScaleNumber(Axis axis, float coordinateWidth, DataZoom dataZoom = null)
        {
            int splitNum = GetSplitNumber(axis, coordinateWidth, dataZoom);
            if (axis.IsCategory())
            {
                int tick = Mathf.RoundToInt(axis.data.Count * 1f / splitNum);
                return Mathf.CeilToInt(axis.data.Count * 1.0f / tick) + 1;
            }
            else
            {
                return splitNum + 1;
            }
        }

        /// <summary>
        /// 获得分割段宽度
        /// </summary>
        /// <param name="coordinateWidth"></param>
        /// <param name="dataZoom"></param>
        /// <returns></returns>
        internal static float GetScaleWidth(Axis axis, float coordinateWidth, int index, DataZoom dataZoom = null)
        {
            if (index < 0) return 0;
            int num = GetScaleNumber(axis, coordinateWidth, dataZoom);
            int splitNum = GetSplitNumber(axis, coordinateWidth, dataZoom);
            if (num <= 0) num = 1;
            if (axis.type == Axis.AxisType.Value && axis.interval > 0)
            {
                if (axis.runtimeMinMaxRange <= 0) return 0;
                if (index >= splitNum) return coordinateWidth - (index - 1) * axis.interval * coordinateWidth / axis.runtimeMinMaxRange;
                else return axis.interval * coordinateWidth / axis.runtimeMinMaxRange;
            }
            else
            {
                if (axis.IsCategory() && axis.data.Count > 0)
                {
                    int tick = Mathf.RoundToInt(axis.data.Count * 1f / splitNum);
                    var count = axis.boundaryGap ? axis.data.Count : axis.data.Count - 1;
                    if (count <= 0) return 0;
                    var each = coordinateWidth / count;
                    if (index >= num - 1)
                    {
                        if (axis.axisTick.alignWithLabel) return each * tick;
                        else return coordinateWidth - each * tick * (index - 1);
                    }
                    else return each * tick;
                }
                else
                {
                    if (splitNum <= 0) return 0;
                    else return coordinateWidth / splitNum;
                }
            }
        }

        internal static float GetEachWidth(Axis axis, float coordinateWidth, DataZoom dataZoom = null)
        {
            if (axis.data.Count > 0)
            {
                var count = axis.boundaryGap ? axis.data.Count : axis.data.Count - 1;
                return count > 0 ? coordinateWidth / count : coordinateWidth;
            }
            else
            {
                int num = GetScaleNumber(axis, coordinateWidth, dataZoom) - 1;
                return num > 0 ? coordinateWidth / num : coordinateWidth;
            }
        }

        /// <summary>
        /// 调整最大最小值
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        internal static void AdjustMinMaxValue(Axis axis, ref float minValue, ref float maxValue, bool needFormat)
        {
            if (axis.type == Axis.AxisType.Log)
            {
                int minSplit = 0;
                int maxSplit = 0;
                maxValue = ChartHelper.GetMaxLogValue(maxValue, axis.logBase, axis.logBaseE, out maxSplit);
                minValue = ChartHelper.GetMinLogValue(minValue, axis.logBase, axis.logBaseE, out minSplit);
                axis.splitNumber = (minSplit > 0 && maxSplit > 0) ? (maxSplit + minSplit - 1) : (maxSplit + minSplit);
                return;
            }
            if (axis.minMaxType == Axis.AxisMinMaxType.Custom)
            {
                if (axis.min != 0 || axis.max != 0)
                {
                    if (axis.inverse)
                    {
                        minValue = -axis.max;
                        maxValue = -axis.min;
                    }
                    else
                    {
                        minValue = axis.min;
                        maxValue = axis.max;
                    }
                }
            }
            else
            {
                switch (axis.minMaxType)
                {
                    case Axis.AxisMinMaxType.Default:
                        if (minValue == 0 && maxValue == 0)
                        {
                        }
                        else if (minValue > 0 && maxValue > 0)
                        {
                            minValue = 0;
                            maxValue = needFormat ? ChartHelper.GetMaxDivisibleValue(maxValue, axis.ceilRate) : maxValue;
                        }
                        else if (minValue < 0 && maxValue < 0)
                        {
                            minValue = needFormat ? ChartHelper.GetMinDivisibleValue(minValue, axis.ceilRate) : minValue;
                            maxValue = 0;
                        }
                        else
                        {
                            minValue = needFormat ? ChartHelper.GetMinDivisibleValue(minValue, axis.ceilRate) : minValue;
                            maxValue = needFormat ? ChartHelper.GetMaxDivisibleValue(maxValue, axis.ceilRate) : maxValue;
                        }
                        break;
                    case Axis.AxisMinMaxType.MinMax:
                        minValue = needFormat ? ChartHelper.GetMinDivisibleValue(minValue, axis.ceilRate) : minValue;
                        maxValue = needFormat ? ChartHelper.GetMaxDivisibleValue(maxValue, axis.ceilRate) : maxValue;
                        break;
                }
            }
            var tempRange = maxValue - minValue;
            if (axis.runtimeMinMaxRange != tempRange)
            {
                axis.runtimeMinMaxRange = tempRange;
                if (axis.type == Axis.AxisType.Value && axis.interval > 0)
                {
                    axis.SetComponentDirty();
                }
            }
        }

        internal static bool NeedShowSplit(Axis axis)
        {
            if (!axis.show) return false;
            if (axis.IsCategory() && axis.data.Count <= 0) return false;
            else if (axis.IsValue() && axis.runtimeMinValue == 0 && axis.runtimeMaxValue == 0) return false;
            else return true;
        }

        internal static void AdjustCircleLabelPos(Text txt, Vector3 pos, Vector3 cenPos, float txtHig, Vector3 offset)
        {
            var txtWidth = txt.preferredWidth;
            var sizeDelta = new Vector2(txtWidth, txt.preferredHeight);
            txt.GetComponent<RectTransform>().sizeDelta = sizeDelta;
            var diff = pos.x - cenPos.x;
            if (diff < -1f) //left
            {
                pos = new Vector3(pos.x - txtWidth / 2, pos.y);
            }
            else if (diff > 1f) //right
            {
                pos = new Vector3(pos.x + txtWidth / 2, pos.y);
            }
            else
            {
                float y = pos.y > cenPos.y ? pos.y + txtHig / 2 : pos.y - txtHig / 2;
                pos = new Vector3(pos.x, y);
            }
            txt.transform.localPosition = pos + offset;
        }

        internal static void AdjustRadiusAxisLabelPos(Text txt, Vector3 pos, Vector3 cenPos, float txtHig, Vector3 offset)
        {
            var txtWidth = txt.preferredWidth;
            var sizeDelta = new Vector2(txtWidth, txt.preferredHeight);
            txt.GetComponent<RectTransform>().sizeDelta = sizeDelta;
            var diff = pos.y - cenPos.y;
            if (diff > 20f) //left
            {
                pos = new Vector3(pos.x - txtWidth / 2, pos.y);
            }
            else if (diff < -20f) //right
            {
                pos = new Vector3(pos.x + txtWidth / 2, pos.y);
            }
            else
            {
                float y = pos.y > cenPos.y ? pos.y + txtHig / 2 : pos.y - txtHig / 2;
                pos = new Vector3(pos.x, y);
            }
            txt.transform.localPosition = pos;
        }
    }
}
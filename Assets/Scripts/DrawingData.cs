using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingData
{
    public string Name { get; set; }
    public TimeSpan TimeSpentDrawing { get; set; }
    public string DrawingMethodsAllowed { get; set; }
    public int NumberOfLinesDrawn { get { return LineDataList.Count; } set { NumberOfLinesDrawn = value; } }
    public List<LineData> LineDataList { get; set; }

    public DrawingData()
    {
        LineDataList = new List<LineData>();
    }
}

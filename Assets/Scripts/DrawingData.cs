using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingData
{
    public string Name {get; set;}
    public TimeSpan TimeSpentDrawing {get; set;}
    public string DrawingMethodsAllowed {get; set;}
    public int NumberOfLinesDrawn {get; set;}
    public List<LineData> LineDataList
    {
        get { return LineDataList; }
        set
        {
            LineDataList = value;
            NumberOfLinesDrawn = LineDataList.Count;
        }
    }
}

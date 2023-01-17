using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingData
{
    private string _name;
    private TimeSpan _timeSpentDrawing;
    private string _drawingMethodsAllowed;
    private int _numberOfLinesDrawn;
    private List<LineData> _lineDataList
    {
        get { return _lineDataList; }
        set
        {
            _lineDataList = value;
            _numberOfLinesDrawn = _lineDataList.Count;
        }
    }
}

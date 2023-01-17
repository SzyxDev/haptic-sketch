using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineData
{
    private TimeSpan _lineTime { get; set; }
    private List<Vector3> _linePoints { get; set; }
    private string _drawingMethod { get; set; }
}

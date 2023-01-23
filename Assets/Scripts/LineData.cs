using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineData
{
    public TimeSpan LineTime { get; set; }
    public List<Vector3> LinePoints { get; set; }
    public string DrawingMethod { get; set; }
}

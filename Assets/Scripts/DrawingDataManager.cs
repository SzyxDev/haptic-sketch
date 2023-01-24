using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingDataManager : MonoBehaviour
{
    public List<DrawingData> DrawingDataList { get; set; }

    void Start()
    {
        DrawingDataList = new List<DrawingData>();
    }

    public DrawingData GetCurrentDrawingData()
    {
        return DrawingDataList[DrawingDataList.Count - 1];
    }

    public DrawingData CreateNewDrawingData()
    {
        DrawingData drawingData = new DrawingData();
        DrawingDataList.Add(drawingData);
        return drawingData;
    }
}

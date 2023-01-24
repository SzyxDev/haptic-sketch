using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCollector
{
    private TimeManager _timeManager;
    private List<DrawingData> _drawingDataList;
    private string _id;

    private string _delimiter = ";";

    public void SaveDataToFiles(TimeManager timeManager, List<DrawingData> drawingDataList, string id)
    {
        _id = id;
        _timeManager = timeManager;
        _drawingDataList = drawingDataList;
        SaveGeneralInfoFile();
        SaveDrawingData();
    }

    private void SaveGeneralInfoFile()
    {
        string fileName = _id + "-GeneralInfo";
        List<string> lines = new List<string>();
        lines.Add(dataEntry("Id", "OverallTime", "DrawingTime", "IntroTime"));
        lines.Add(dataEntry(_id, getTimeSpanAsHourMinutesSeconds(_timeManager.OverallTime), getTimeSpanAsHourMinutesSeconds(_timeManager.OverallDrawTime), getTimeSpanAsHourMinutesSeconds(_timeManager.IntroTime)));
        File.WriteAllLines(fileName + ".txt", lines);
    }

    private void SaveDrawingData()
    {
        int i = 1;
        foreach (DrawingData drawingData in _drawingDataList)
        {
            string fileName = _id + "-" + drawingData.Name;
            List<string> lines = new List<string>();
            lines.Add(dataEntry("Id", "Name", "DrawingMethods", "Order", "DrawingTime", "NumberOfLines"));
            lines.Add(dataEntry(_id, drawingData.Name, drawingData.DrawingMethodsAllowed, i.ToString(), getTimeSpanAsHourMinutesSeconds(drawingData.TimeSpentDrawing), drawingData.NumberOfLinesDrawn.ToString()));
            File.WriteAllLines(fileName + ".txt", lines);

            int j = 0;
            lines.Clear();
            lines.Add(dataEntry("LineId", "DrawingMethod", "TimeUsed"));
            foreach (LineData lineData in drawingData.LineDataList)
            {
                lines.Add(getLineDataInfoEntry(lineData, j));
                saveLineDataPoints(lineData, fileName, j);
                j++;
            }
            File.WriteAllLines(fileName + "-Info" + ".txt", lines);
            i++;
        }
    }

    private string getLineDataInfoEntry(LineData lineData, int lineId)
    {
        return dataEntry(lineId.ToString(), lineData.DrawingMethod, getTimeSpanAsHourMinutesSeconds(lineData.LineTime));
    }

    private void saveLineDataPoints(LineData lineData, string name, int lineNumber)
    {
        string fileName = name + "-" + lineNumber;
        List<string> lines = new List<string>();
        lines.Add(dataEntry("X", "Y", "Z"));
        foreach (Vector3 points in lineData.LinePoints)
        {
            lines.Add(dataEntry(points.x.ToString(), points.y.ToString(), points.z.ToString()));
        }
        File.WriteAllLines(fileName + ".txt", lines);
    }

    private string getTimeSpanAsHourMinutesSeconds(TimeSpan timeSpan)
    {
        return timeSpan.ToString(@"hh\:mm\:ss");
    }

    private string dataEntry(params string[] input)
    {
        return String.Join(_delimiter, input);
    }
}

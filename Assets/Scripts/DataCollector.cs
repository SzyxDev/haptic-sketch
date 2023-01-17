using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    [Tooltip("Unique Name every DataFile will use")]
    public string Id;
    private TimeManager _timeManager;
    private List<DrawingData> _drawingDataList;

    private string _delimiter = ";";

    public void SaveDataToFiles()
    {

    }

    private void SaveGeneralInfoFile()
    {
        string fileName = Id + "-GeneralInfo.txt";
        List<string> lines = new List<string>();
        lines.Add(dataEntry("Id", "OverallTime", "DrawingTime", "IntroTime"));
        //lines.Add(_timeManager.OverallTime.Duration)
        File.WriteAllLines(fileName, lines);
    }

    private string dataEntry(params string[] input)
    {
        return String.Join(_delimiter, input);
    }
}

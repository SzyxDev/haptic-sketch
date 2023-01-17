using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    private DateTime _overallTimeStart;
    public TimeSpan OverallTime { get; set; }
    private DateTime _drawTimeStart;
    public TimeSpan DrawTime { get; set; }
    private DateTime _introTimeStart;
    public TimeSpan IntroTime { get; set; }
    private DateTime _lineTimeStart;
    public List<TimeSpan> LineTimes = new List<TimeSpan>();

    public void StartOverallTimer()
    {
        _overallTimeStart = DateTime.Now;
    }

    public void StopOverallTimer()
    {
        OverallTime = DateTime.Now.Subtract(_overallTimeStart);
    }

    public void StartDrawTimer()
    {
        _drawTimeStart = DateTime.Now;
    }

    public void StopDrawTimer()
    {
        DrawTime = DateTime.Now.Subtract(_drawTimeStart);
    }

    public void StartIntroTimer()
    {
        _introTimeStart = DateTime.Now;
    }

    public void StopIntroTimer()
    {
        IntroTime = DateTime.Now.Subtract(_introTimeStart);
    }

    public void StartLineTimer()
    {
        _lineTimeStart = DateTime.Now;
    }

    public void StopLineTimer()
    {
        LineTimes.Add(DateTime.Now.Subtract(_lineTimeStart));
    }

}

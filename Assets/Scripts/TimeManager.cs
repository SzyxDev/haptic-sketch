using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private DateTime _overallTimeStart;
    public TimeSpan OverallTime { get; set; }
    private DateTime _drawTimeStart;
    private DateTime _overallDrawTimeStart;
    public TimeSpan OverallDrawTime { get; set; }
    private DateTime _introTimeStart;
    public TimeSpan IntroTime { get; set; }
    private DateTime _lineTimeStart;

    public void StartOverallTimer()
    {
        _overallTimeStart = DateTime.Now;
    }

    public void StopOverallTimer()
    {
        OverallTime = DateTime.Now.Subtract(_overallTimeStart);
    }

    public void StartOverallDrawTimer()
    {
        _overallDrawTimeStart = DateTime.Now;
    }

    public void StopOverallDrawTimer()
    {
        OverallDrawTime = DateTime.Now.Subtract(_overallDrawTimeStart);
    }

    public void StartDrawTimer()
    {
        _drawTimeStart = DateTime.Now;
    }

    public TimeSpan StopDrawTimer()
    {
        return DateTime.Now.Subtract(_drawTimeStart);
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

    public TimeSpan StopLineTimer()
    {
        return DateTime.Now.Subtract(_lineTimeStart);
    }

}

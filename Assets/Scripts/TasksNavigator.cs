using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksNavigator : MonoBehaviour
{

    public GameObject MessageBox;
    public GameObject NextButton;
    public GameObject TemplateBase;
    public int LatinSquareId;

    private UnityEngine.UI.Text _text;
    private int _counter;
    private int _latinSquareCounter;
    private TemplateRenderer _templateRenderer;
    private DrawingManager _drawingManager;
    private TimeManager _timeManager;
    private DataCollector _dataCollector;
    private DrawingDataManager _drawingDataManager;

    // Start is called before the first frame update
    void Start()
    {
        _templateRenderer = new TemplateRenderer();
        _drawingManager = GetComponent<DrawingManager>();
        _timeManager = GetComponent<TimeManager>();
        _dataCollector = GetComponent<DataCollector>();
        _drawingDataManager = GetComponent<DrawingDataManager>();
        _text = MessageBox.GetComponent<UnityEngine.UI.Text>();
        _text.text = Messages.Intro;
        _counter = 7;
        _latinSquareCounter = 0;
        _timeManager.StartOverallTimer();
        _timeManager.StartIntroTimer();
    }


    public void SwitchIntroText()
    {
        switch (_counter)
        {
            case 7:
                _text.text = Messages.Intro;
                break;
            case 8:
                _text.text = Messages.Instructions1;
                break;
            case 9:
                _text.text = Messages.Instructions2;
                _drawingManager.SetAllowedDrawingInteraction(DrawingManager.DrawingInteraction.Both);
                break;
            case 1:
                // Square
                _timeManager.StopIntroTimer();
                _timeManager.StartOverallDrawTimer();
                updateStartData("Air", "Square");
                _text.text = Messages.TasksInActionMidAir;
                _drawingManager.SetAllowedDrawingInteraction(DrawingManager.DrawingInteraction.MidAir);
                break;
            case 2:
                // Cube
                updateStartData("Air", "Cube");
                _text.text = Messages.TasksInActionSurface;
                _drawingManager.SetAllowedDrawingInteraction(DrawingManager.DrawingInteraction.Surface);
                break;
            case 3:
                // Circle
                _text.text = Messages.TasksInActionMidAir;
                _drawingManager.SetAllowedDrawingInteraction(DrawingManager.DrawingInteraction.MidAir);
                break;
            case 4:
                // Sphere
                _text.text = Messages.TasksInActionSurface;
                _drawingManager.SetAllowedDrawingInteraction(DrawingManager.DrawingInteraction.Surface);
                break;
            case 5:
                // Triangle
                break;

            case 6:
                // Pyramid
                break;
            default:
                updateStopData();
                _timeManager.StopOverallDrawTimer();
                _timeManager.StopOverallTimer();
                _dataCollector.SaveDataToFiles(_timeManager, _drawingDataManager.DrawingDataList);
                _counter = 0;
                break;
        }

        if (_counter < 9) 
        {
            _counter++;
        } 
        else
        {
            _counter = BalancedLatinSquare.Shapes[LatinSquareId,_latinSquareCounter];
            _latinSquareCounter++;
        }
    }

    private string getAllowedMethods(int id)
    {
        return "";
    }

    private void updateStartData(string allowedDrawingInteraction, string name)
    {
        _timeManager.StartDrawTimer();
        DrawingData drawingData = _drawingDataManager.CreateNewDrawingData();
        drawingData.DrawingMethodsAllowed = allowedDrawingInteraction;
        drawingData.Name = name;
    }

    private void updateStopData()
    {
        TimeSpan drawTime = _timeManager.StopDrawTimer();
        _drawingDataManager.GetCurrentDrawingData().TimeSpentDrawing = drawTime;
    }

}

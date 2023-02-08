using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksNavigator : MonoBehaviour
{

    public GameObject MessageBox;

    [Tooltip("Unique Name every DataFile will use")]
    public string Id;
    public int LatinSquareId;
    public int MethodId;

    [Tooltip("en or de allowed")]
    public string Locale;

    private UnityEngine.UI.Text _text;
    private int _counter;
    private int _latinSquareCounter;
    private TemplateRenderer _templateRenderer;
    private DrawingManager _drawingManager;
    private TimeManager _timeManager;
    private DataCollector _dataCollector;
    private DrawingDataManager _drawingDataManager;
    private DrawingManager.DrawingInteraction _drawingInteraction;
    private bool _isDrawingActive;

    private int c = 0;

    // Start is called before the first frame update
    void Start()
    {
        _templateRenderer = new TemplateRenderer();
        _drawingManager = GetComponent<DrawingManager>();
        _timeManager = GetComponent<TimeManager>();
        _dataCollector = new DataCollector();
        _drawingDataManager = GetComponent<DrawingDataManager>();
        _text = MessageBox.GetComponent<UnityEngine.UI.Text>();
        _text.text = Locale == "de" ? Messages.IntroDe : Messages.IntroEn;
        _counter = 8;
        _latinSquareCounter = 0;
        _timeManager.StartOverallTimer();
        _timeManager.StartIntroTimer();
        _isDrawingActive = false;
    }


    public void SwitchIntroText()
    {
        if (_counter < 8 && _drawingDataManager.GetCurrentDrawingData() != null && _drawingDataManager.GetCurrentDrawingData().LineDataList.Count == 0)
        {
            return;
        }
        if (_isDrawingActive)
        {
            updateStopData();
            _isDrawingActive = false;
        }
        switch (_counter)
        {
            case 8:
                _text.text = Locale == "de" ? Messages.Instructions1De : Messages.Instructions1En;
                _text.text += "\n" + getDrawingInteractionText();
                break;
            case 9:
                _text.text = Locale == "de" ? Messages.Instructions2De : Messages.Instructions2En;
                _drawingManager.SetAllowedDrawingInteraction(DrawingManager.DrawingInteraction.HapticSurface);
                break;
            case 1:
                // Square
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.SquareDe : Messages.SquareEn;
                updateStartData(_drawingInteraction.ToString(), "Square");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 2:
                // Cube
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.CubeDe : Messages.CubeEn;
                updateStartData(_drawingInteraction.ToString(), "Cube");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 3:
                // Circle
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.CircleDe : Messages.CircleEn;
                updateStartData(_drawingInteraction.ToString(), "Circle");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 4:
                // Sphere
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.SphereDe : Messages.SphereEn;
                updateStartData(_drawingInteraction.ToString(), "Sphere");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 5:
                // Triangle
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.TriangleDe : Messages.TriangleEn;
                updateStartData(_drawingInteraction.ToString(), "Triangle");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 6:
                // Pyramid
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.PyramidDe : Messages.PyramidEn;
                updateStartData(_drawingInteraction.ToString(), "Pyramid");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 0:
                _text.text = Locale == "de" ? Messages.EndDe : Messages.EndEn;
                _timeManager.StopOverallDrawTimer();
                _timeManager.StopOverallTimer();
                _dataCollector.SaveDataToFiles(_timeManager, _drawingDataManager.DrawingDataList, Id, _drawingInteraction.ToString());
                _counter = 0;
                break;
            default:
                break;
        }
        updateCounters();
    }

    private DrawingManager.DrawingInteraction getDrawingInteraction()
    {
        if (MethodId == 0)
        {
            return DrawingManager.DrawingInteraction.Midair;
        }
        else if (MethodId == 1)
        {
            return DrawingManager.DrawingInteraction.HapticSurface;
        }
        else if (MethodId == 2)
        {
            return DrawingManager.DrawingInteraction.VirtualSurface;
        }

        return DrawingManager.DrawingInteraction.HapticSurface;
    }

    private string getDrawingInteractionText()
    {
        if (MethodId == (int)DrawingManager.DrawingInteraction.Midair && Locale == "en")
        {
            return Messages.TasksInActionMidAirEn;
        }
        else if (MethodId == (int)DrawingManager.DrawingInteraction.Midair && Locale == "de")
        {
            return Messages.TasksInActionMidAirDe;
        }
        else if (MethodId == (int)DrawingManager.DrawingInteraction.HapticSurface && Locale == "en")
        {
            return Messages.TasksInActionSurfaceEn;
        }
        else if (MethodId == (int)DrawingManager.DrawingInteraction.HapticSurface && Locale == "de")
        {
            return Messages.TasksInActionSurfaceDe;
        }
        else if (MethodId == (int)DrawingManager.DrawingInteraction.VirtualSurface && Locale == "en")
        {
            return Messages.TasksInActionControllerEn;
        }
        else if (MethodId == (int)DrawingManager.DrawingInteraction.VirtualSurface && Locale == "de")
        {
            return Messages.TasksInActionControllerDe;
        }

        return "ERROR: NO VALID DRAWING INTERACTION";
    }

    private void updateCounters()
    {
        Debug.Log("Lat: " + _latinSquareCounter);
        Debug.Log("Count: " + _counter);
        if (_counter <= 0)
        {
            _counter = -1;
            return;
        }
        if (_counter < 9 && _counter >= 8)
        {
            _counter++;
        }
        else
        {
            if (_latinSquareCounter == 0)
            {
                _timeManager.StopIntroTimer();
                _timeManager.StartOverallDrawTimer();
            }
            else if (_latinSquareCounter >= BalancedLatinSquare.Shapes.GetLength(1))
            {
                Debug.Log("End");
                _counter = 0;
                _latinSquareCounter = 0;
                return;
            }
            _counter = BalancedLatinSquare.Shapes[LatinSquareId, _latinSquareCounter];
            _latinSquareCounter++;
        }
    }

    private void updateStartData(string allowedDrawingInteraction, string name)
    {
        _timeManager.StartDrawTimer();
        DrawingData drawingData = _drawingDataManager.CreateNewDrawingData();
        drawingData.DrawingMethodsAllowed = allowedDrawingInteraction;
        drawingData.Name = name;
        _isDrawingActive = true;
    }

    private void updateStopData()
    {
        TimeSpan drawTime = _timeManager.StopDrawTimer();
        _drawingDataManager.GetCurrentDrawingData().TimeSpentDrawing = drawTime;
    }

}

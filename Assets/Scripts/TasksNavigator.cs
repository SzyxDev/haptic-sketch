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
    public bool ThreeDrawingMethods;

    [Tooltip("en or de allowed")]
    public string Locale;

    private UnityEngine.UI.Text _text;
    private int _counter;
    private int _latinSquareCounter;
    private int _methodCounter;
    private int _methodCounter2;
    private TemplateRenderer _templateRenderer;
    private DrawingManager _drawingManager;
    private TimeManager _timeManager;
    private DataCollector _dataCollector;
    private DrawingDataManager _drawingDataManager;
    private DrawingManager.DrawingInteraction _drawingInteraction;

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
        _methodCounter = 0;
        _methodCounter2 = 0;
        _latinSquareCounter = 0;
        _timeManager.StartOverallTimer();
        _timeManager.StartIntroTimer();
    }


    public void SwitchIntroText()
    {
        switch (_counter)
        {
            case 8:
                _text.text = Messages.Instructions1;
                break;
            case 9:
                _text.text = Messages.Instructions2;
                _drawingManager.SetAllowedDrawingInteraction(DrawingManager.DrawingInteraction.Both);
                break;
            case 1:
                // Square
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.SquareDe : Messages.SquareEn;
                _text.text += "\n" + getDrawingInteractionText(_drawingInteraction);
                updateStartData(_drawingInteraction.ToString(), "Square");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 2:
                // Cube
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.CubeDe : Messages.CubeEn;
                _text.text += "\n" + getDrawingInteractionText(_drawingInteraction);
                updateStartData(_drawingInteraction.ToString(), "Cube");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 3:
                // Circle
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.CircleDe : Messages.CircleEn;
                _text.text += "\n" + getDrawingInteractionText(_drawingInteraction);
                updateStartData(_drawingInteraction.ToString(), "Circle");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 4:
                // Sphere
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.SphereDe : Messages.SphereEn;
                _text.text += "\n" + getDrawingInteractionText(_drawingInteraction);
                updateStartData(_drawingInteraction.ToString(), "Sphere");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 5:
                // Triangle
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.TriangleDe : Messages.TriangleEn;
                _text.text += "\n" + getDrawingInteractionText(_drawingInteraction);
                updateStartData(_drawingInteraction.ToString(), "Triangle");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            case 6:
                // Pyramid
                _drawingInteraction = getDrawingInteraction();
                _text.text = Locale == "de" ? Messages.PyramidDe : Messages.PyramidEn;
                _text.text += "\n" + getDrawingInteractionText(_drawingInteraction);
                updateStartData(_drawingInteraction.ToString(), "Pyramid");
                _drawingManager.SetAllowedDrawingInteraction(_drawingInteraction);
                break;
            default:
                updateStopData();
                _timeManager.StopOverallDrawTimer();
                _timeManager.StopOverallTimer();
                _dataCollector.SaveDataToFiles(_timeManager, _drawingDataManager.DrawingDataList, Id);
                _counter = 0;
                break;
        }
        updateCounters();
    }

    private DrawingManager.DrawingInteraction getDrawingInteraction()
    {
        int drawingInteractionId = ThreeDrawingMethods ? BalancedLatinSquare.Methods3[_methodCounter, _methodCounter2] : BalancedLatinSquare.Methods2[_methodCounter, _methodCounter2];

        if (drawingInteractionId == 1)
        {
            return DrawingManager.DrawingInteraction.MidAir;
        }
        else if (drawingInteractionId == 2)
        {
            return DrawingManager.DrawingInteraction.Both;
        }
        else if (drawingInteractionId == 3)
        {
            return DrawingManager.DrawingInteraction.Board;
        }

        return DrawingManager.DrawingInteraction.Both;
    }

    private string getDrawingInteractionText(DrawingManager.DrawingInteraction drawingInteraction)
    {
        if (drawingInteraction == DrawingManager.DrawingInteraction.MidAir && Locale == "en")
        {
            return Messages.TasksInActionMidAirEn;
        }
        else if (drawingInteraction == DrawingManager.DrawingInteraction.MidAir && Locale == "de")
        {
            return Messages.TasksInActionMidAirDe;
        }
        else if (drawingInteraction == DrawingManager.DrawingInteraction.Both && Locale == "en")
        {
            return Messages.TasksInActionSurfaceEn;
        }
        else if (drawingInteraction == DrawingManager.DrawingInteraction.Both && Locale == "de")
        {
            return Messages.TasksInActionSurfaceDe;
        }

        return "ERROR: NO VALID DRAWING INTERACTION";
    }

    private void updateCounters()
    {
        if (_counter < 9)
        {
            _counter++;
        }
        else
        {
            if ((ThreeDrawingMethods && _methodCounter2 == 3) || (!ThreeDrawingMethods && _methodCounter2 == 2))
            {
                if (_latinSquareCounter == 0 && _methodCounter == 0)
                {
                    _timeManager.StopIntroTimer();
                    _timeManager.StartOverallDrawTimer();
                }
                else if (_latinSquareCounter >= BalancedLatinSquare.Shapes.Length - 1)
                {
                    _counter = 0;
                    _methodCounter = 0;
                    _methodCounter2 = 0;
                    return;
                }
                _counter = BalancedLatinSquare.Shapes[LatinSquareId, _latinSquareCounter];
                _latinSquareCounter++;

                if (ThreeDrawingMethods && _methodCounter <= BalancedLatinSquare.Methods3.Length - 1)
                {
                    _methodCounter++;
                }
                else
                {
                    _methodCounter = 0;
                }

                if (!ThreeDrawingMethods && _methodCounter <= BalancedLatinSquare.Methods2.Length - 1)
                {
                    _methodCounter++;
                }
                else
                {
                    _methodCounter = 0;
                }


                _methodCounter2 = 0;
            }
            else
            {
                _methodCounter2++;
            }
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

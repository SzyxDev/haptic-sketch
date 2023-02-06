using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public enum DrawingInteraction
    {
        MidAir,
        Both,
        Controller,
        Surface,
        None
    }

    private DrawingInteraction _allowedDrawingInteraction;
    private DrawingDataManager _drawingDataManager;

    void Start()
    {
        _allowedDrawingInteraction = DrawingInteraction.None;
        _drawingDataManager = GetComponent<DrawingDataManager>();
    }

    public void SetAllowedDrawingInteraction(DrawingInteraction drawingInteraction)
    {
        DestroyDrawings(false);
        _allowedDrawingInteraction = drawingInteraction;
    }

    public bool IsDrawingAllowed(DrawingInteraction drawingInteraction)
    {
        if (drawingInteraction == DrawingInteraction.MidAir)
        {
            return true;
        }

        return drawingInteraction == _allowedDrawingInteraction || _allowedDrawingInteraction == DrawingInteraction.Both;
    }

    private bool DoesAMaximumOfOneDrawingExist()
    {
        return GameObject.FindGameObjectsWithTag("Drawing").Length <= 1;
    }

    public void DestroyDrawings(bool clearData)
    {
        foreach (GameObject drawing in GameObject.FindGameObjectsWithTag("Drawing"))
        {
            Destroy(drawing);
        }
        if (clearData)
        {
            DrawingData drawingData = _drawingDataManager.GetCurrentDrawingData();
            if (drawingData != null)
            {
                drawingData.LineDataList.Clear();
            }
        }
    }

}

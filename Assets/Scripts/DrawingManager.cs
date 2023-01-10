using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public enum DrawingInteraction
    {
        MidAir,
        Surface,
        Both,
        None
    }

    private DrawingInteraction _allowedDrawingInteraction;

    void Start()
    {
        _allowedDrawingInteraction = DrawingInteraction.None;
    }

    public void SetAllowedDrawingInteraction(DrawingInteraction drawingInteraction)
    {
        DestroyDrawings();
        _allowedDrawingInteraction = drawingInteraction;
    }

    public bool IsDrawingAllowed(DrawingInteraction drawingInteraction)
    {
        return (drawingInteraction == _allowedDrawingInteraction && DoesAMaximumOfOneDrawingExist()) || _allowedDrawingInteraction == DrawingInteraction.Both;
    }

    private bool DoesAMaximumOfOneDrawingExist()
    {
        return GameObject.FindGameObjectsWithTag("Drawing").Length <= 1;
    }

    private void DestroyDrawings()
    {
        foreach (GameObject drawing in GameObject.FindGameObjectsWithTag("Drawing"))
        {
            Destroy(drawing);
        }
    }

}
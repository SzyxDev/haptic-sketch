using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateRenderer
{

    public enum Rotation
    {
        Horizontal,
        Vertical,
        Angled
    }

    private LineRenderer _line;
    private Vector3 _pos;

    public void RenderCircle(GameObject container, Rotation rotation)
    {

        int segments = 360;
        float radius = 0.07f;
        int pointCount = segments + 1;
        _line = container.GetComponent<LineRenderer>();
        _line.positionCount = pointCount;
        _line.useWorldSpace = true;
        _line.startWidth = 0.006f;
        _line.endWidth = 0.006f;

        Vector3[] points = new Vector3[pointCount];
        _pos = new Vector3(container.transform.position.x, container.transform.position.y, container.transform.position.z);

        for (int i = 0; i < pointCount; i++)
        {
            float rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = calcPointBasedOnRotation(rotation, rad, radius);
        }

        _line.SetPositions(points);
    }

    private Vector3 calcPointBasedOnRotation(Rotation rotation, float rad, float radius)
    {
        switch (rotation)
        {
            case Rotation.Horizontal:
                return new Vector3(_pos.x + Mathf.Cos(rad) * radius,
                                    _pos.y + 0,
                                    _pos.z + Mathf.Sin(rad) * radius);
            case Rotation.Vertical:
                return new Vector3(_pos.x + 0,
                                    _pos.y + Mathf.Cos(rad) * radius,
                                    _pos.z + Mathf.Sin(rad) * radius);
            case Rotation.Angled:
                // TODO: Implement angle circle
            default:
                return new Vector3(0f, 0f, 0f);
        }
    }

    public void ResetLine()
    {
        _line.positionCount = 0;
    }
}

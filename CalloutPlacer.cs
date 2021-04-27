using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Quadrant
{
    TopLeft, TopRight,
    BottomLeft, BottomRight
}

public class CalloutPlacer : MonoBehaviour
{
    public Component componentToPointTo;
    public float deltaX = 0;
    public float deltaY = 0;
    
    void Update()
    {
        if (!gameObject.activeSelf)
            return;
        
        gameObject.transform.position = CalculatePosition();
    }

    public void Show()
    {
        if (!componentToPointTo)
            return;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private Vector3 CalculatePosition()
    {
        var directionToPointFrom = GetOppositeQuadrant(GetQuadrantToPointTo());
        var deltaX = CalculateDeltaX(directionToPointFrom);
        var deltaY = CalculateDeltaY(directionToPointFrom);
        var t = componentToPointTo.transform.position;
        var p = gameObject.transform.position;

        return new Vector3(t.x + deltaX, t.y + deltaY, p.z);
    }

    private float CalculateDeltaX(Quadrant quadrant)
    {
        var w = deltaX;
        switch (quadrant)
        {
            case Quadrant.TopRight:
            case Quadrant.BottomRight:
                w *= -1;
                break;
        }

        return w;
    }
    
    private float CalculateDeltaY(Quadrant quadrant)
    {
        var h = deltaY;
        switch (quadrant)
        {
            case Quadrant.BottomLeft:
            case Quadrant.BottomRight:
                h *= -1;
                break;
        }

        return h;
    }

    private Quadrant GetQuadrantToPointTo()
    {
        const int xMidpoint = 10;
        const int yMidpoint = 10;

        var position = componentToPointTo.gameObject.transform.position;

        return position.y > yMidpoint
            ? ( position.x <= xMidpoint ? Quadrant.TopLeft : Quadrant.TopRight )
            : ( position.x <= xMidpoint ? Quadrant.BottomLeft : Quadrant.BottomRight );
    }

    private static Quadrant GetOppositeQuadrant(Quadrant input)
    {
        switch (input)
        {
            case Quadrant.TopLeft: return Quadrant.BottomRight;
            case Quadrant.TopRight: return Quadrant.BottomLeft;
            case Quadrant.BottomLeft: return Quadrant.TopRight;
            case Quadrant.BottomRight: return Quadrant.TopLeft;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, "invalid quadrant");
        }
    }
}
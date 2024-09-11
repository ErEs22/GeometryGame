using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class ShapeGUIInteractive : Image
{
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return GetComponent<PolygonCollider2D>().OverlapPoint(screenPoint);
    }
}
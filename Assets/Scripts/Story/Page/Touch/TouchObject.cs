using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchObject : MonoBehaviour
{
    public event Action<TouchObject>  OnClicked;
    public int countClick;
    public bool isClick=false;
    public Transform blinkEffect;
    public TouchUI touchUI;
    public bool isBlink=true;
    public PolygonCollider2D polygonCollider;

    [Obsolete]
    private void Reset()
    {
        touchUI=GetComponentInChildren<TouchUI>();
        blinkEffect = gameObject.transform.FindChild("SpineBlink");
        polygonCollider=GetComponent<PolygonCollider2D>();
        Vector2 center = GetPolygonColliderCenter(polygonCollider);
        touchUI.GetComponent<RectTransform>().localPosition=center;
        touchUI.Reset();
        blinkEffect.GetComponent<RectTransform>().localPosition = center;
        Debug.Log(center);
    }

    Vector2 GetPolygonColliderCenter(PolygonCollider2D collider)
    {
        Vector2[] points = collider.points;
        Vector2 center = Vector2.zero;

        foreach (Vector2 point in points)
        {
            center += point;
        }

        center /= points.Length;

        return center;
    }
    private void OnMouseDown()
    {
        OnClicked?.Invoke(this);
    }
}

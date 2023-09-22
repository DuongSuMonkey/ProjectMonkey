﻿using System;
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
    public bool hasBlink=true;
    public PolygonCollider2D polygonCollider;

    [Obsolete]
    private void Reset()
    {
        GetTouchUI();
        GetBlinkEffect();
        SetPosition();
    }
    public void GetTouchUI()
    {
        touchUI = GetComponentInChildren<TouchUI>();
    }

    [Obsolete]
    public void GetBlinkEffect()
    {
        blinkEffect = gameObject.transform.FindChild("SpineBlink");
    }
    public void SetPosition()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        Vector2 center = GetPolygonColliderCenter(polygonCollider);
        touchUI.GetComponent<RectTransform>().localPosition = center;
        touchUI.Reset();
        blinkEffect.GetComponent<RectTransform>().localPosition = center;
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
    public void Select(ITouchUIHandler touchUIHandle, List<TouchObject> touchObjects, List<TouchUI> touchesUI,int index)
    {
       isClick = true;
       countClick++;
       touchUIHandle.HideAllTouch();
       if (countClick > 1 || touchesUI[index] != touchUIHandle.GetTouch())
       {
            touchUIHandle.ProcessDoubleClick(this, index);
            return;
       }
       ShowTouchCurrent(touchUIHandle, touchObjects, touchesUI);
    }
    public void ShowTouchCurrent(ITouchUIHandler touchUIHandle,List<TouchObject> touchObjects,List<TouchUI> touchesUI)
    {
        touchUIHandle.ShowTouchCurrent(touchObjects, touchesUI);
    }
}

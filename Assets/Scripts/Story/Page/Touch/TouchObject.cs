using Spine.Unity;
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
    public SkeletonGraphic blinkEffect;
    public TouchUI touchUI;
    public bool hasBlink=true;
    public PolygonCollider2D polygonCollider;

    public void Reset()
    {
        GetTouchUI();
        GetBlinkEffect();
        SetPosition();
        SetChildrenPosition();
    }
    public void GetTouchUI()
    {
        touchUI = GetComponentInChildren<TouchUI>();
    }

    public void GetBlinkEffect()
    {
        blinkEffect = GetComponentInChildren<SkeletonGraphic>();
    }
    public void SetChildrenPosition()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        Vector2 center = GetPolygonColliderCenter(polygonCollider);
        touchUI.GetComponent<RectTransform>().localPosition = center;
        blinkEffect.GetComponent<RectTransform>().localPosition = center;
        touchUI.Reset();
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
    public void Select()
    {
       isClick = true;
       countClick++;
    }
    public void SetAnchors()
    {
       GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
       GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
    }
    public void SetPosition()
    { 
        GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    }
    public void SetScale()
    {
      GetComponent<RectTransform>().localScale = Vector3.one;
    }
}

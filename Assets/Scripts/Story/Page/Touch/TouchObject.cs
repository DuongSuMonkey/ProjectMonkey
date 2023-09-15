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

    [Obsolete]
    private void Reset()
    {
        touchUI=GetComponentInChildren<TouchUI>();
        blinkEffect = gameObject.transform.FindChild("SpineBlink");
        blinkEffect.GetComponent<RectTransform>().position= touchUI.GetComponent<RectTransform>().position;
    }
    private void OnMouseDown()
    {
        OnClicked?.Invoke(this);
    }
}

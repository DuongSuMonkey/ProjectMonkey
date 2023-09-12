using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public event Action<Blink>  OnItemClicked;
    public int countClick;
    public bool isClick=false;
    public Transform blinkEffect;
    public bool isBlink=true;

    [Obsolete]
    private void Reset()
    {
        blinkEffect = gameObject.transform.FindChild("SpineBlink");
    }
    private void OnMouseDown()
    {
        OnItemClicked?.Invoke(this);
    }
}

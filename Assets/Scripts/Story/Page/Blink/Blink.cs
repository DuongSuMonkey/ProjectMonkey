using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class Blink : MonoBehaviour
{
    public event Action<Blink>  OnItemClicked;
    public int countClick;
    public bool isClick=false;
   private void OnMouseDown()
    {
        OnItemClicked?.Invoke(this);
    }
}

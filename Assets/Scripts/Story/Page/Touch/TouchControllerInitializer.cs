using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TouchControllerInitializer :ITouchControllerInitializer
{
    private ITouchManager touchManager;
    public  TouchControllerInitializer(ITouchManager touchManager)
    {
        this.touchManager = touchManager;
    }

    [Obsolete]
    public void LoadComponents(List<TouchUI> touchesUI, List<TouchObject> touchObjects,MonoBehaviour obj)
    {
        LoadTouchObject(touchObjects, obj);
        LoadTouchesUI(touchesUI,touchObjects);
        SetAnchors(touchObjects);
        SetPosition(touchObjects);
        SetScale(touchObjects);
    }
    public void LoadTouchObject(List<TouchObject> touchObjects,MonoBehaviour obj)
    {
        var touchObjectsArray = obj.GetComponentsInChildren<TouchObject>();
        List<TouchObject> canBlinkObjects = new List<TouchObject>();
        List<TouchObject> cannotBlinkObjects = new List<TouchObject>();

        foreach (var touchObject in touchObjectsArray)
        {
            if (touchObject.hasBlink)
            {
                canBlinkObjects.Add(touchObject);
            }
            else
            {
                cannotBlinkObjects.Add(touchObject);
            }
        }

        for (int i = canBlinkObjects.Count - 1; i >= 0; i--)
        {
            touchObjects.Add(canBlinkObjects[i]);
        }
        touchObjects.AddRange(cannotBlinkObjects);
    }
    public void LoadTouchesUI(List<TouchUI> touchesUI, List<TouchObject> touchObjects)
    {
        foreach (var touchObject in touchObjects)
        {
            touchesUI.Add(touchObject.GetComponentInChildren<TouchUI>());
        }
    }

    public void SetAnchors(List<TouchObject> touchObjects)
    {

        foreach (var touchObject in touchObjects)
        {
            touchObject.SetAnchors();
        }
    }

    public void SetPosition(List<TouchObject> touchObjects)
    {
        foreach (var touchUI in touchObjects)
        {
            touchUI.SetPosition();
        }
    }

    public void SetScale(List<TouchObject> touchObjects)
    {
        foreach (var touchUI in touchObjects)
        {
            touchUI.SetScale();
        }
    }
}

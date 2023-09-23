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

    [Obsolete]
    public void LoadTouchObject(List<TouchObject> touchObjects,MonoBehaviour obj)
    {
        touchManager.LoadTouchObjects(touchObjects, obj);
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

    public void HideAllTouchesUI(List<TouchUI> touchObjects)
    {
        foreach (var touch in touchObjects)
        {
            touch.HideTouch();
        }
    }

}

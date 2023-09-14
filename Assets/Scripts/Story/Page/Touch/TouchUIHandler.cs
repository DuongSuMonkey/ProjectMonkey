using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TouchUIHandler:ITouchUIHandler
{
    private List<TouchUI> touchesUI;
    private int currentIndex;
    private List<TouchUI> existingTouches;
    private ITouchManager touchManager;
    private IBlinkController blinkController;
    public TouchUIHandler(List<TouchUI> touchesUI, int currentIndex, List<TouchUI> existingTouches)
    {
        this.touchesUI = touchesUI;
        this.currentIndex = currentIndex;
        this.existingTouches= existingTouches;
        touchManager=new TouchManager(touchesUI,currentIndex, existingTouches);
        blinkController=new BlinkController(touchesUI,currentIndex);
    }
    public void ShowTouchCurrent(List<TouchObject> touchObjects, List<TouchUI> touchesUI)
    {
        if (!blinkController.IsBlinkFinal(touchObjects))
        {
            return;
        }
        blinkController.HideBlinkEffect(touchObjects[currentIndex]);
        touchManager.CreateTouch(touchObjects[currentIndex], currentIndex);
        ShowBlinkNext(touchObjects);
    }
    private int IncreaseIndex()
    {
        return currentIndex++;
    }

    public void ShowBlinkNext(List<TouchObject> touchObjects)
    {
        if (blinkController.CanNextBlink(touchObjects))
        {
            IncreaseIndex();
            blinkController.ShowBlink(touchObjects);
        }
    }
    public void ProcessDoubleClick(TouchObject touchObject, int index)
    {
        blinkController.HideBlinkEffect(touchObject);
        HideAllTouch();
        CreateTouch(touchObject ,index);
    }
    public TouchUI GetTouch()
    {
        return touchesUI[currentIndex];
    }
    public void CreateTouch(TouchObject touchObject, int index)
    {
        touchManager.CreateTouch(touchObject, index);
    }
    public void HideAllTouch()
    {
        touchManager.HideAllTouch();
    }

}


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
        touchManager =new TouchManager(touchesUI,currentIndex, existingTouches);
        blinkController = new BlinkController(touchesUI,currentIndex);
    }
    public void ShowTouchCurrent(List<TouchObject> touchObjects, List<TouchUI> touchesUI)
    {
        if (!IsProcessingRemaining(touchObjects))
        {
            return;
        }
        blinkController.HideBlinkEffect(touchObjects[currentIndex]);
        CreateTouch(touchObjects[currentIndex], currentIndex);
        ShowBlinkNext(touchObjects);
    }
    public bool IsProcessingRemaining(List<TouchObject> touchObjects)
    {
        return blinkController.IsProcessingRemaining(touchObjects);
    }
    private int IncreaseIndex()
    {
        return currentIndex++;
    }
    public void ShowBlinkNext(List<TouchObject> touchObjects)
    {
        if (CanNextBlink(touchObjects))
        {
            if (touchObjects[currentIndex].isBlink)
            {
                IncreaseIndex();
            }
          blinkController.ShowBlink(touchObjects);
        }
    }
    public void ShowBlink(List<TouchObject> touchObjects)
    {
        blinkController.ShowBlink(touchObjects);
    }
    public bool CanNextBlink(List<TouchObject> touchObjects)
    {
         return blinkController.CanNextBlink(touchObjects);
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


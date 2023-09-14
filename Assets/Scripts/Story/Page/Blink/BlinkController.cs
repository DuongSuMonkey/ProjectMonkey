using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkController : IBlinkController
{
    private List<TouchUI> touchesUI;
    private int currentIndex;
    public BlinkController(List<TouchUI> touchesUI, int currentIndex)
    {
        this.touchesUI = touchesUI;
        this.currentIndex = currentIndex;
    }

    public TouchObject GetTouchObject(TouchObject touchObject)
    {
        return touchObject;
    }

    public void HideBlinkEffect(TouchObject touchObject)
    {
        touchObject.blinkEffect.gameObject.SetActive(false);
    }

    public void ShowBlinkEffect(TouchObject touchObject)
    {
        touchObject.blinkEffect.gameObject.SetActive(true);
    }
    public bool IsBlinkFinal(List<TouchObject> touchObjects)
    {
        return currentIndex < touchObjects.Count;
    }

    public bool CanNextBlink(List<TouchObject> touchObjects)
    {
        return currentIndex < touchObjects.Count - 1;
    }
    public void ShowBlink(List<TouchObject> touchObjects)
    {
        ProcessClickBlink(touchObjects);
        if (touchObjects[currentIndex].isBlink)
        {
            ShowBlinkEffect(touchObjects[currentIndex]);
        }
    }
    private void ProcessClickBlink(List<TouchObject> touchObjects)
    {
        if (IsBlinkFinal(touchObjects))
        {
            if (touchObjects[currentIndex].isClick)
            {
                currentIndex++;
                ProcessClickBlink(touchObjects);
            }
        }
    }
}
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
        CanBlink(touchObjects);
        if (touchObjects[currentIndex].isBlink)
        {
            ShowBlinkEffect(touchObjects[currentIndex]);
        }
    }
    public void CanBlink(List<TouchObject> touchObjects)
    {
        if (CanNextBlink(touchObjects))
        {
            if (touchObjects[currentIndex].isClick || !touchObjects[currentIndex].isBlink)
            {
                currentIndex++;
                CanBlink(touchObjects);
            }
        }
    }
}
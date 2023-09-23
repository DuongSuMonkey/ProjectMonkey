using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkController : IBlinkController
{
    private List<TouchUI> touchesUI;
    private int currentIndex;
    private List<TouchObject> touchObjects;
    public BlinkController(List<TouchUI> touchesUI, int currentIndex, List<TouchObject> touchObjects)
    {
        this.touchesUI = touchesUI;
        this.currentIndex = currentIndex;
        this.touchObjects = touchObjects;
    }

    public void HideBlinkEffect(TouchObject touchObject)
    {
        touchObject.blinkEffect.gameObject.SetActive(false);
    }

    public void ShowBlinkEffect(TouchObject touchObject)
    {
        touchObject.blinkEffect.gameObject.SetActive(true);
    }
    public bool IsProcessingRemaining(int currentIndex, List<TouchObject> touchObjects)
    {
        return currentIndex < touchObjects.Count;
    }
    public void HideAllBlinks(List<TouchObject> touchObjects)
    {
        foreach (var touchObject in touchObjects)
        {
            HideBlinkEffect(touchObject);
        }
    }
    public bool CanNextBlink(int currentIndex,List<TouchObject> touchObjects)
    {
        return currentIndex < touchObjects.Count - 1;
    }
    public void ShowBlink(List<TouchObject> touchObjects)
    {
        CanBlink(touchObjects);
        if (touchObjects[currentIndex].hasBlink)
        {
            ShowBlinkEffect(touchObjects[currentIndex]);
        }
    }
    public void CanBlink(List<TouchObject> touchObjects)
    {
        if (CanNextBlink(currentIndex,touchObjects))
        {
            if (touchObjects[currentIndex].isClick || !touchObjects[currentIndex].hasBlink)
            {
                currentIndex++;
                CanBlink(touchObjects);
            }
        }
    }
    public void ShowFirstBlink(List<TouchObject> touchObjects, List<TouchUI> existingTouches)
    {
        for (int i = 0; i < touchObjects.Count; i++)
        {
            if (touchObjects[i].hasBlink)
            {
                touchObjects[i].blinkEffect.gameObject.SetActive(true);
                currentIndex = i;
                break;
            }
        }
    }
}
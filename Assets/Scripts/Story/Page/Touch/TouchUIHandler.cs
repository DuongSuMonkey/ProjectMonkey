using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TouchUIHandler:ITouchUIHandler
{
    private List<TouchObject> touchObjects;
    private List<TouchUI> touchesUI;
    private int currentIndex;
    private List<TouchUI> existingTouches;
    private IBlinkController blinkController;
    private ITouchSelection touchSelection;
    public TouchUIHandler(List<TouchUI> touchesUI, int currentIndex, List<TouchUI> existingTouches, List<TouchObject> touchObjects)
    {
        this.touchesUI = touchesUI;
        this.currentIndex = currentIndex;
        this.existingTouches = existingTouches;
        touchSelection = new TouchSelection(existingTouches,touchObjects,touchesUI);
        blinkController = new BlinkController(touchesUI, currentIndex,touchObjects);
        this.touchObjects = touchObjects;
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
        return blinkController.IsProcessingRemaining(currentIndex,touchObjects);
    }
    private int IncreaseIndex()
    {
        return currentIndex++;
    }
    public void ShowBlinkNext(List<TouchObject> touchObjects)
    {
        if (CanNextBlink(touchObjects))
        {
            if (touchObjects[currentIndex].isClick || !touchObjects[currentIndex].hasBlink)
            {
                IncreaseIndex();
                ShowBlinkNext(touchObjects);

            }
            ShowBlink(touchObjects);
        }
        
    }
    public void ShowBlink(List<TouchObject> touchObjects)
    {
        blinkController.ShowBlink(touchObjects);
    }
    public bool CanNextBlink(List<TouchObject> touchObjects)
    {
         return blinkController.CanNextBlink(currentIndex,touchObjects);
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
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TouchUI touch = UnityEngine.Object.Instantiate(touchesUI[index], new Vector3(canvasPos.x, canvasPos.y, 0),
           Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(-15, 15))));
        touch.gameObject.transform.SetParent(touchObject.transform);
        touch.gameObject.transform.localScale = Vector3.one;
        touchSelection.SelectTouchUI(touch);
        touch.DestroyTouchCoroutine();
    }

    public void HideAllTouch()
    {
        foreach (TouchUI touch in existingTouches)
        {
            if (touch != null)
            {
                touch.HideTouch();
            }
        }
    }
    public void ShowFirstBlink(List<TouchObject> touchObjects, List<TouchUI> existingTouches)
    {
        blinkController.ShowFirstBlink(touchObjects, existingTouches);
    }
    public void HideAllBlinks(List<TouchObject> touchObjects)
    {
        blinkController.HideAllBlinks(touchObjects);
    }

    public void HideAllTouchesUI(List<TouchUI> touchesUI)
    {
        foreach (var touch in touchesUI)
        {
            touch.HideTouch();
        }
    }
}


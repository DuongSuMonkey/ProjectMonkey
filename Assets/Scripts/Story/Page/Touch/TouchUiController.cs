using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TouchUIController:ITouchUIController
{
    private List<TouchUI> touches;
    private int currentIndex;
    private List<TouchUI> existingTouches=new List<TouchUI>();
    private ITouchManager touchManager;
    private ITouchSelection touchSelection;
    private IBlinkController blinkController;
    public TouchUIController(List<TouchUI> touches, int currentIndex)
    {
        this.touches = touches;
        this.currentIndex = currentIndex;
        touchSelection=new TouchSelection(existingTouches);
        touchManager=new TouchManager(touches,currentIndex, existingTouches);
        blinkController=new BlinkController(touches);
    }
    private void SelectCurrentTouchUI(TouchUI touchUI)
    {
        touchSelection.SelectTouchUI(touchUI);
    }
    private void HideBlinkEffect(Blink blink)
    {
        blinkController.HideBlinkEffect(blink);
    }
    public void InvokeShowTouchNext(MonoBehaviour obj,float delay)
    {
        obj.Invoke(nameof(ShowTouchNext),delay);
    }
    public void ShowTouchCurrent(List<Blink> blinks, List<TouchUI> touches, MonoBehaviour obj)
    {
        if (IsTouchFinal(blinks))
        {
            HideBlinkEffect(blinks[currentIndex]);
            SelectCurrentTouchUI(touches[currentIndex]);
            InvokeShowTouchNext(obj,touches[currentIndex].audioClip.length);
        }

    }
    private void HideCurrentTouch()
    {
        touches[currentIndex].HideTouch();
    }

    private void ShowNextBlink(List<Blink> blinks)
    {
        ProcessClickBlink(blinks);
        if (blinks[currentIndex].isBlink)
        {
            blinkController.ShowBlinkEffect(blinks[currentIndex]);
        }
        
    }
    private void ProcessClickBlink(List<Blink> blinks)
    {
        if (IsTouchFinal(blinks))
        {
            if (blinks[currentIndex].isClick)
            {
                currentIndex++;
                ProcessClickBlink(blinks);
            }
        }
    }
    private int IncreaseIndex()
    {
        return currentIndex++;
    }
    public void ShowTouchNext(List<Blink> blinks)
    {
        HideCurrentTouch();
        if (IsNextTouch( blinks))
        {
            IncreaseIndex();
            ShowNextBlink(blinks);
        }
    }
    public bool IsTouchFinal(List<Blink> blinks)
    {
        return currentIndex < blinks.Count;
    }

    public bool IsNextTouch(List<Blink> blinks)
    {
        return currentIndex < blinks.Count - 1;
    }
    public void ProcessDoubleClick(Blink blink, int index)
    {
        HideAllTouch();
        blink.blinkEffect.gameObject.SetActive(false);
        CreateTouch(blink ,index);
    }
    public TouchUI GetTouch()
    {
        return touches[currentIndex];
    }
    public void CreateTouch(Blink blink, int index)
    {
        touchManager.CreateTouch(blink, index);
    }
    public void HideAllTouch()
    {
        touchManager.HideAlTouch(existingTouches);
    }

}


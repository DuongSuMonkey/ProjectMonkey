using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkHandler : IBlinkHandler
{
    private int currentIndex;
    private List<TouchObject> touchObjects;
    private bool canShowFirstBlink;
    private IHideBlinkEffect hideBlinkEffect;
    private IShowBlinkEffect showBlinkEffect;
    public BlinkHandler(int currentIndex, List<TouchObject> touchObjects)
    {
        this.currentIndex = currentIndex;
        this.touchObjects = touchObjects;
        canShowFirstBlink = true;
        hideBlinkEffect=new HideBlinkEffect();
        showBlinkEffect=new ShowBlinkEffect();
        HideAllBlinks(touchObjects);
    }
    public void HideAllBlinks(List<TouchObject> touchObjects)
    {
        hideBlinkEffect.HideAllBlinks(touchObjects);
    }
    public bool CanNextBlink()
    {
        return currentIndex < touchObjects.Count - 1;
    }
    public void ShowFirstBlink(IPageController pageController)
    {
        if (pageController.IsSyncFinish() && canShowFirstBlink)
        {
            GetFirstBlink();
        }
    }

    public void GetFirstBlink()
    {
        for (int i = 0; i < touchObjects.Count; i++)
        {
            if (touchObjects[i].hasBlink)
            {
                touchObjects[i].blinkEffect.gameObject.SetActive(true);
                currentIndex = i;
                canShowFirstBlink = false;
                break;
            }
        }
    }

    public void Select()
    {
        hideBlinkEffect.HideBlink(touchObjects[currentIndex]);
        ShowBlinkNext();
    }
    public void ShowBlinkNext()
    {
        if (CanNextBlink())
        {
            if (touchObjects[currentIndex].isClick || !touchObjects[currentIndex].hasBlink)
            {
                currentIndex++;
                ShowBlinkNext();

            }
            showBlinkEffect.ShowBlink(currentIndex, touchObjects);
        }
    }

    public void OnTouchSelected(TouchObject touchObject)
    {
        Select();
    }
}
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

    public TouchUIController(List<TouchUI> touches, int currentIndex)
    {
        this.touches = touches;
        this.currentIndex = currentIndex;
    }
    private void SelectCurrentTouchUI(TouchUI touchUI)
    {
        touchUI.Select();
    }
    private void HideBlinkImage(Blink blink)
    {
        blink.gameObject.GetComponent<Image>().enabled = false;
    }
    public void InvokeShowTouchNext(MonoBehaviour obj,float delay)
    {
        obj.Invoke(nameof(ShowTouchNext),delay);
    }
    public void ShowTouchCurrent(List<Blink> blinks, List<TouchUI> touches, MonoBehaviour obj)
    {
        if (IsTouchFinal(blinks))
        {
            HideBlinkImage(blinks[currentIndex]);
            SelectCurrentTouchUI(touches[currentIndex]);
            InvokeShowTouchNext(obj,touches[currentIndex].audioClip.length);
        }

    }
    private void HideCurrentTouch()
    {
        touches[currentIndex].gameObject.SetActive(false);
    }

    private void ShowNextTouch(List<Blink> blinks)
    {
        blinks[currentIndex].GetComponent<Image>().enabled = true;
    }
    private int IncreaseIndex()
    {
        return currentIndex++;
    }
    public void ShowTouchNext(List<Blink> blinks)
    {  
        if (IsNextTouch( blinks))
        {
            HideCurrentTouch();
            IncreaseIndex();
            ShowNextTouch(blinks);
        }
        else
        {
            HideCurrentTouch();
        }
    }

    public  void SearchText(TouchUI touch,List<TextMeshProUGUI> txtsContent)
    {
        for (int i = 0; i < txtsContent.Count; i++)
        {
            string textcontent = Regex.Replace(txtsContent[i].text, @"[,.;!?]", "");
            if (touch.TxtContent.text == textcontent)
            {
                txtsContent[i].color = UnityEngine.Color.red;
                txtsContent[i].GetComponent<RectTransform>().anchoredPosition = new Vector2
                    (txtsContent[i].GetComponent<RectTransform>().anchoredPosition.x,
                    txtsContent[i].GetComponent<RectTransform>().anchoredPosition.y + 6);
            }
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
}


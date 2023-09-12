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
    public TouchUIController(List<TouchUI> touches, int currentIndex)
    {
        this.touches = touches;
        this.currentIndex = currentIndex;
    }
    private void SelectCurrentTouchUI(TouchUI touchUI)
    {
        existingTouches.Add(touchUI);
        touchUI.Select();
    }
    private void HideBlinkEffect(Blink blink)
    {
        blink.blinkEffect.gameObject.SetActive( false);
    }
    public void InvokeShowTouchNext(MonoBehaviour obj,float delay)
    {
        obj.Invoke(nameof(ShowTouchNext),delay);
    }
    public Blink GetBlink(List<Blink> blinks)
    {
        return blinks[currentIndex];
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
        touches[currentIndex].background.enabled = false;
        touches[currentIndex].txtContent.enabled = false;
    }

    private void ShowNextTouch(List<Blink> blinks)
    {
        
        if (blinks[currentIndex].isClick)
        {
                currentIndex++;
                ShowNextTouch(blinks);
        }
        if (blinks[currentIndex].isBlink)
        {
            blinks[currentIndex].blinkEffect.gameObject.SetActive(true);
        }
        
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

    public  void SearchText(TouchUI touch,List<TextMeshProUGUI> txtsContent, MonoBehaviour obj)
    {
        for (int i = 0; i < txtsContent.Count; i++)
        {
            string textcontent = Regex.Replace(txtsContent[i].text, @"[,.;!?]", "");
            if (touch.TxtContent.text == textcontent)
            {
                txtsContent[i].color = UnityEngine.Color.red;
                txtsContent[i].GetComponent<Animator>().SetTrigger("isCheck");
                obj.StartCoroutine(OriginalTextColorCoroutine(txtsContent[i]));
            }
        }
    }

    public IEnumerator OriginalTextColorCoroutine(TextMeshProUGUI textContent)
    {
        yield return new WaitForSeconds(1f); 
        OriginalTextColor(textContent);
    }
    public void OriginalTextColor(TextMeshProUGUI textcontent)
    {
        if (textcontent.color == UnityEngine.Color.red)
        {
            textcontent.color = UnityEngine.Color.black;
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
        if (existingTouches.Count >= 1)
        {
            for (int i = 0; i < existingTouches.Count; i++) // Bắt đầu từ i = 1 để bỏ qua this.transform
            {
                if (existingTouches[i] != null)
                {
                    existingTouches[i].background.enabled = false;
                    existingTouches[i].txtContent.enabled = false;
                }
            }
        }
        blink.blinkEffect.gameObject.SetActive(false);
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TouchUI touch = Object.Instantiate(touches[index], new Vector3(canvasPos.x, canvasPos.y, 0), Quaternion.identity);
        existingTouches.Add(touch);
        touch.gameObject.transform.SetParent(blink.transform);
        touch.gameObject.transform.localScale = Vector3.one;
        touch.Select();
        touch.StartCoroutineDestroyTouch();
    }
}


//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Text.RegularExpressions;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;

//public class TouchesController : Texts
//{
//    [SerializeField] private List<Blink> blinks;
//    [SerializeField] private List<TouchUI> touches;
//    [SerializeField] private PageController pageController;
//    [SerializeField] private bool isFirst=true;
//    private void Start()
//    {
//        AddEventTouch();
//        HideAllTouches();
//        HideAllBlinks();
//    }
//    private void Reset()
//    {
//        LoadComponents();
//    }
//    private void LoadComponents()
//    {
//        LoadBlinks();
//        LoadTouches();
//        LoadPageController();
//        LoadTexts();
//    }

//    private void LoadBlinks()
//    {
//        blinks.AddRange(GetComponentsInChildren<Blink>());
//    }

//    private void LoadTouches()
//    {
//        foreach (var blink in blinks)
//        {
//            touches.Add(blink.GetComponentInChildren<TouchUI>());
//        }
//    }

//    private void LoadPageController()
//    {
//        pageController = GetComponentInParent<PageController>();
//    }

//    private void LoadTexts()
//    {
//        foreach (var changeTextColor in pageController.ChangeTextColor)
//        {
//            TextMeshProUGUI[] texts = changeTextColor.GetComponentsInChildren<TextMeshProUGUI>();
//            AddText(texts);
//        }
//    }

//    private void HideAllTouches()
//    {
//        foreach (var touch in touches)
//        {
//            touch.gameObject.SetActive(false);
//        }
//    }

//    private void HideAllBlinks()
//    {
//        foreach (var blink in blinks)
//        {
//            blink.GetComponent<Image>().enabled = false;
//        }
//    }
//    private void Update()
//    {
//        if (pageController.ChangeTextColor[pageController.ChangeTextColor.Count -1].IsFinal && isFirst)
//        {
//            blinks[0].gameObject.GetComponent<Image>().enabled = true;
//            isFirst = false;
//            return;
//        }
//    }

//    public void AddEventTouch()
//    {
//        foreach(var blink in blinks)
//         blink.OnItemClicked += HandleItemSelection;
//    }

//    private void HandleItemSelection(Blink blink)
//    {
//        if (pageController.ChangeTextColor[pageController.ChangeTextColor.Count - 1].IsFinal)
//        {
//            int index = blinks.IndexOf(blink);
//            blink.countClick++;
//            if (blink.countClick > 1)
//            {
//                ProcessDoubleClick(blink, index);
//            }
//            else
//            {
//                ShowTextCurrent();
//                SearchText(touches[currentIndex]);
//            }
//        }
//    }
//    private void ProcessDoubleClick(Blink blink, int index)
//    {
//        blinks[index].gameObject.GetComponent<Image>().enabled = false;
//        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        TouchUI touch = Instantiate(touches[index], new Vector3(canvasPos.x, canvasPos.y, 0), Quaternion.identity);
//        touch.gameObject.transform.SetParent(blink.transform);
//        touch.gameObject.transform.localScale = Vector3.one;
//        touch.Select();
//        touch.GetDestroy();
//    }
//    public void ShowTouchNext()
//    {
//        if (currentIndex < touches.Count - 1)
//        {
//            touches[currentIndex].gameObject.SetActive(false); 
//            currentIndex++;
//            blinks[currentIndex].gameObject.GetComponent<Image>().enabled = true;
//        }
//        else
//        {
//            touches[currentIndex].gameObject.SetActive(false);
//        }
//    }
//    public void ShowTextCurrent()
//    {
//        if (currentIndex < touches.Count)
//        {
//            blinks[currentIndex].gameObject.GetComponent<Image>().enabled = false;
//            touches[currentIndex].Select();
//            Invoke(nameof(ShowTouchNext), touches[currentIndex].audioClip.length);
//        }

//    }
//    public void SearchText(TouchUI touch)
//    {
//        for (int i = 0; i < txtsContent.Count; i++)
//        {
//            string textcontent= Regex.Replace(txtsContent[i].text, @"[,.;!?]", "");
//            if (touch.TxtContent.text == textcontent)
//            {
//                txtsContent[i].color = UnityEngine.Color.red;
//            }
//        }
//    }

//}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;

public class TouchesController : Texts
{
    [SerializeField] private List<Blink> blinks;
    [SerializeField] private List<TouchUI> touches;
    [SerializeField] private PageController pageController;
    [SerializeField] private bool isFirst = true;

    private IBlinkController blinkController;
    private ITouchUIController touchUIController;
    private void Start()
    {
        blinkController = new BlinkController(blinks, touches);
        touchUIController = new TouchUIController(touches, currentIndex);
        AddEventBlink();
        HideAllTouches();
        HideAllBlinks();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        LoadPageController();
        LoadBlinks();
        LoadTouches();
        LoadTexts();
    }

    private void LoadBlinks()
    {
        blinks.AddRange(GetComponentsInChildren<Blink>());
    }

    private void LoadTouches()
    {
        foreach (var blink in blinks)
        {
            touches.Add(blink.GetComponentInChildren<TouchUI>());
        }
    }

    private void LoadPageController()
    {
        pageController = GetComponentInParent<PageController>();
    }

    public override void LoadTexts()
    {
        foreach (var changeTextColor in pageController.ChangeTextColor)
        {
            TextMeshProUGUI[] texts = changeTextColor.GetComponentsInChildren<TextMeshProUGUI>();
            AddText(texts);
        }
    }

    private void HideAllTouches()
    {
        foreach (var touch in touches)
        {
            touch.gameObject.SetActive(false);
        }
    }

    private void HideAllBlinks()
    {
        foreach (var blink in blinks)
        {
            blink.GetComponent<Image>().enabled = false;
        }
    }

    private void Update()
    {
        if (pageController.IsFinal() && isFirst)
        {
            blinks[0].gameObject.GetComponent<Image>().enabled = true;
            isFirst = false;
        }
    }

    private void AddEventBlink()
    {
        foreach (var blink in blinks)
        {
            blink.OnItemClicked += HandleItemSelection;
        }
    }

    private void HandleItemSelection(Blink blink)
    {
        if (IsClick())
        {
            int index = blinks.IndexOf(blink);
            blink.isClick=true;
            blink.countClick++;
            touchUIController.SearchText(touches[index], txtsContent,this);
            if (blink.countClick>1)
            {
                blinkController.ProcessDoubleClick(blink, index);
            }
            else
            {

                touchUIController.ShowTouchCurrent(blinks, touches, this);
            }
        }
    }
    public bool IsClick()
    {
        return pageController.ChangeTextColor[pageController.ChangeTextColor.Count - 1].IsFinal;
    }
    public void ShowTouchNext()
    {
       touchUIController.ShowTouchNext(blinks); 
    }
    public void ShowTextCurrent()
    {
        touchUIController.ShowTouchCurrent(blinks,touches,this);
    }
}


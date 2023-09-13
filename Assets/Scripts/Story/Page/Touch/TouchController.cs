using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Reflection;

public class TouchesController : Texts
{
    [SerializeField] private List<Blink> blinks;
    [SerializeField] private List<TouchUI> touches;
    [SerializeField] private PageController pageController;
    [SerializeField] private bool isFirst = true;
    [SerializeField] private ITouchUIController touchUIController;
    [SerializeField] private ISearchText searchTextController;

    private void Start()
    {
        searchTextController = new SearchTextController();
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
            touch.HideTouch();
        }
    }

    private void HideAllBlinks()
    {
        foreach (var blink in blinks)
        {
            blink.blinkEffect.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (pageController.IsFinal() && isFirst)
        {
            blinks[0].blinkEffect.gameObject.SetActive (true);
            isFirst = false;
        }
    }

    private void AddEventBlink()
    {
        foreach (var blink in blinks)
        {
            blink.OnClicked += HandleTouchSelection;
        }
    }

    private void HandleTouchSelection(Blink blink)
    {
        if (IsClick())
        {
            int index = blinks.IndexOf(blink);
            TouchSelection(blink, index);
        }
    }
    private void TouchSelection(Blink blink,int index)
    {
        blink.isClick = true;
        blink.countClick++;
        SearchText(touches[index]);
        if (blink.countClick > 1 || touches[index] != touchUIController.GetTouch())
        {
            ProcessDoubleClick(blink, index);
            return;
        }
        ShowTextCurrent();
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
    private void SearchText(TouchUI touch)
    {
        searchTextController.Search(touch, txtsContent, this);
    }
    private void ProcessDoubleClick(Blink blink, int index)
    {
        touchUIController.ProcessDoubleClick(blink, index);
    }
}


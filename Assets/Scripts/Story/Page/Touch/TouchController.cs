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
    [SerializeField] private List<TouchObject> touchObjects;
    [SerializeField] private List<TouchUI> touchesUI;
    [SerializeField] private PageController pageController;
    [SerializeField] private bool isFirst = true;
    [SerializeField] private ITouchUIHandler touchUIController;
    [SerializeField] private ISearchText searchTextController;
    [SerializeField] private List<TouchUI> existingTouches=new List<TouchUI>();

    private void Start()
    {
        searchTextController = new SearchTextController();
        touchUIController = new TouchUIHandler(touchesUI, currentIndex, existingTouches);
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
        touchObjects.AddRange(GetComponentsInChildren<TouchObject>());
    }

    private void LoadTouches()
    {
        foreach (var blink in touchObjects)
        {
            touchesUI.Add(blink.GetComponentInChildren<TouchUI>());
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
        foreach (var touch in touchesUI)
        {
            touch.HideTouch();
        }
    }

    private void HideAllBlinks()
    {
        foreach (var blink in touchObjects)
        {
            blink.blinkEffect.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (pageController.IsFinal() && isFirst)
        {
            touchObjects[0].blinkEffect.gameObject.SetActive (true);
            isFirst = false;
        }
    }

    private void AddEventBlink()
    {
        foreach (var blink in touchObjects)
        {
            blink.OnClicked += HandleTouchSelection;
        }
    }

    private void HandleTouchSelection(TouchObject touchObject)
    {
        if (IsClick())
        {
            int index = this.touchObjects.IndexOf(touchObject);
            TouchSelection(touchObject, index);
        }
    }
    private void TouchSelection(TouchObject touchObject,int index)
    {
       
        touchObject.isClick = true;
        touchObject.countClick++;
        SearchText(touchesUI[index]);
        HideAllTouch();
        if (touchObject.countClick > 1 || touchesUI[index] != touchUIController.GetTouch())
        {
            ProcessDoubleClick(touchObject, index);
            return;
        }
        ShowTouchCurrent();
    }
    public bool IsClick()
    {
        return pageController.ChangeTextColor[pageController.ChangeTextColor.Count - 1].IsFinal;
    }
    public void ShowBlinkNext()
    {
       touchUIController.ShowBlinkNext(touchObjects); 
    }
    public void ShowTouchCurrent()
    {
        touchUIController.ShowTouchCurrent(touchObjects,touchesUI);
    }
    private void SearchText(TouchUI touch)
    {
        searchTextController.Search(touch, txtsContent, this);
    }
    private void ProcessDoubleClick(TouchObject touchObject, int index)
    {
        touchUIController.ProcessDoubleClick(touchObject, index);
    }
    public void HideAllTouch()
    {
        touchUIController.HideAllTouch();
    }
}


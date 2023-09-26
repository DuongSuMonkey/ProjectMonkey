using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Reflection;

public class TouchController : Texts, ITouchController
{
    [SerializeField] private List<TouchObject> touchObjects;
    [SerializeField] private List<TouchUI> touchesUI;
    [SerializeField] private List<TouchUI> existingTouches = new List<TouchUI>();
    [SerializeField] private PageController pageController;
    [SerializeField] private ITouchUIHandler touchUIHandler;
    [SerializeField] private ISearchText searchText;
    [SerializeField] private IAddEventTouchObject addEventTouch;
    [SerializeField] private IBlinkHandler blinkHandler;
    [SerializeField] private ITouchControllerInitializer touchControllerInitializer;
    private void Start()
    {
        searchText = new SearchText();
        touchUIHandler = new TouchUIHandler(touchesUI, currentIndex, existingTouches, touchObjects);
        addEventTouch = new AddEventTouchObject();
        blinkHandler = new BlinkHandler(touchesUI, currentIndex, touchObjects);
        AddEventTouch();
    }
    private void Reset()
    {
        addEventTouch = new AddEventTouchObject();
        touchControllerInitializer = new TouchControllerInitializer();
        LoadComponents();
    }
    public void LoadComponents()
    {
        touchControllerInitializer.LoadComponents(touchesUI, touchObjects, this);
        LoadPageController();
        LoadTexts();
    }

    private void LoadPageController()
    {
        pageController = GetComponentInParent<PageController>();
    }
    public override void LoadTexts()
    {
        pageController.LoadTexts(txtContents);
    }
    public void AddEventTouch()
    {
        addEventTouch.AddEventTouch(touchObjects, HandleTouchSelection);
    }
    public void HandleTouchSelection(TouchObject touchObject)
    {
        int index = touchObjects.IndexOf(touchObject);
        TouchSelection(touchObject, index);
    }
    private void TouchSelection(TouchObject touchObject, int index)
    {
        touchUIHandler.Select(touchObject,index);
        blinkHandler.Select();
        searchText.Search(touchesUI[index], txtContents, this);
    } 
    private void Update()
    {
        blinkHandler.ShowFirstBlink(pageController);
    }
}


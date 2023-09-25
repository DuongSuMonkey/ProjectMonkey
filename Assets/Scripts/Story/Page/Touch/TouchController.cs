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
    [SerializeField] private ISearchText searchTextController;
    [SerializeField] private ITouchManager touchManager;
    [SerializeField] private IBlinkController blinkController;
    [SerializeField] private ITouchControllerInitializer touchControllerInitializer;
    private void Start()
    {
        searchTextController = new SearchTextController();
        touchUIHandler = new TouchUIHandler(touchesUI, currentIndex, existingTouches, touchObjects);
        touchManager = new TouchManager(touchesUI, currentIndex, existingTouches, touchObjects, touchUIHandler);
        blinkController = new BlinkController(touchesUI, currentIndex, touchObjects);
        AddEventTouch();
        HideAllTouchesUI();
        HideAllBlinks();
    }
    private void Reset()
    {
        touchManager = new TouchManager(touchesUI, currentIndex, existingTouches, touchObjects, touchUIHandler);
        touchControllerInitializer = new TouchControllerInitializer(touchManager);
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
    private void HideAllTouchesUI()
    {
        touchUIHandler.HideAllTouchesUI(touchesUI);
    }

    private void HideAllBlinks()
    {
        blinkController.HideAllBlinks(touchObjects);
    }
    public void AddEventTouch()
    {
        touchManager.AddEventTouch(touchObjects, HandleTouchSelection);
    }
    private void HandleTouchSelection(TouchObject touchObject)
    {
        int index = touchObjects.IndexOf(touchObject);
        TouchSelection(touchObject, index);
    }
    private void TouchSelection(TouchObject touchObject, int index)
    {
        touchManager.SelectTouchObject(touchObject, index, pageController);
        searchTextController.Search(touchesUI[index], txtContents, this);
    } 
    private void Update()
    {
        blinkController.UpdateFirstBlink(pageController);
    }
}


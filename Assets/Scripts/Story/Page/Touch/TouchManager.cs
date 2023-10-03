using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Reflection;

public class TouchManager : Texts, ITouchManager, ITouchSubject
{
    [SerializeField] private List<TouchObject> touchObjects;
    [SerializeField] private List<TouchUI> touchesUI;
    [SerializeField] private List<TouchUI> existingTouches = new List<TouchUI>();
    [SerializeField] private IPageController pageController;
    [SerializeField] private ITouchUIHandler touchUIHandler;
    [SerializeField] private ISearchText searchText;
    [SerializeField] private IAddEventTouchObject addEventTouch;
    [SerializeField] private IBlinkHandler blinkHandler;
    [SerializeField] private ITouchControllerInitializer touchControllerInitializer;
    [SerializeField] private List<ITouchObserver> touchObservers = new List<ITouchObserver>();
    private void Start()
    {
        pageController = GetComponentInParent<IPageController>();
        searchText = new SearchText(txtContents, this, touchObjects);
        touchUIHandler = new TouchUIHandler(touchesUI, currentIndex, existingTouches, touchObjects);
        addEventTouch = new AddEventTouchObject();
        blinkHandler = new BlinkHandler(currentIndex, touchObjects);
        AddEventTouch();
        AddObserver(searchText);
        AddObserver(touchUIHandler);
        AddObserver(blinkHandler);
    }
    private void Reset()
    {
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
        pageController = GetComponentInParent<IPageController>();
    }
    public override void LoadTexts()
    {
        List<TextMeshProUGUI> contents= new List<TextMeshProUGUI>();
        foreach(var syncText in pageController.getSyncTexts())
        {
            contents.AddRange(syncText.txtContents);
        }
        foreach(var text in contents)
        {
            txtContents.Add(text);
        }
    }
    public void AddEventTouch()
    {
        addEventTouch.AddEventTouch(touchObjects, HandleTouchSelection);
    }
    public void HandleTouchSelection(TouchObject touchObject)
    {
        NotifyObservers(touchObject);
    }
    private void Update()
    {
        blinkHandler.ShowFirstBlink(pageController);
    }
    public void AddObserver(ITouchObserver observer)
    {
        touchObservers.Add(observer);
    }

    public void RemoveObserver(ITouchObserver observer)
    {
        touchObservers.Remove(observer);
    }

    public void NotifyObservers(TouchObject touchObject)
    {
        foreach (ITouchObserver observer in touchObservers)
        {
            observer.OnTouchSelected(touchObject);
        }
    }
}


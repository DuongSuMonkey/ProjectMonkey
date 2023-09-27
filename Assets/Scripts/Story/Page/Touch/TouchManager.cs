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
    [SerializeField] private PageController pageController;
    [SerializeField] private ITouchUIHandler touchUIHandler;
    [SerializeField] private ISearchText searchText;
    [SerializeField] private IAddEventTouchObject addEventTouch;
    [SerializeField] private IBlinkHandler blinkHandler;
    [SerializeField] private ITouchControllerInitializer touchControllerInitializer;
    [SerializeField] private List<ITouchObserver> touchObservers = new List<ITouchObserver>();
    private void Start()
    {
        searchText = new SearchText(txtContents, this, touchObjects);
        touchUIHandler = new TouchUIHandler(touchesUI, currentIndex, existingTouches, touchObjects);
        addEventTouch = new AddEventTouchObject();
        blinkHandler = new BlinkHandler(currentIndex, touchObjects);
        AddEventTouch();
        AddObserver(searchText as ITouchObserver);
        AddObserver(touchUIHandler as ITouchObserver);
        AddObserver(blinkHandler as ITouchObserver);
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
        NotifyObservers(touchObject);
    }
    //private void TouchSelection(TouchObject touchObject)
    //{
    //    int index = touchObjects.IndexOf(touchObject);
    //    touchUIHandler.Select(touchObject,index);
    //    blinkHandler.Select();
    //    searchText.Search(touchObject);
    //} 
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


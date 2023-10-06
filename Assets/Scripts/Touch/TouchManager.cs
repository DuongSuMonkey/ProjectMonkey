using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Reflection;

public class TouchManager : MonoBehaviour, ITouchManager, ITouchSubject
{
    [SerializeField] public List<TextMeshProUGUI> txtContents;
    [SerializeField] private List<TouchObject> touchObjects;
    [SerializeField] private List<TouchUI> touchesUI;
    [SerializeField] public List<TouchUI> existingTouches = new List<TouchUI>();
    [SerializeField] private IPageController pageController;
    [SerializeField] private ITouchUIHandler touchUIHandler;
    [SerializeField] private ISearchText searchText;
    [SerializeField] private IAddEventTouchObject addEventTouch;
    [SerializeField] private IBlinkHandler blinkHandler;
    [SerializeField] private ITouchControllerInitializer touchControllerInitializer;
    [SerializeField] private ISpawnerTouchUI spawnerTouchUI;
    [SerializeField] private List<ITouchObserver> touchObservers = new List<ITouchObserver>();
    [SerializeField] private int currentIndex = 0;
    private void Start()
    {
        pageController = GetComponentInParent<IPageController>();
        searchText = new SearchText(txtContents, this, touchObjects,pageController);
        touchUIHandler = new TouchUIHandler(touchesUI, existingTouches,pageController);
        addEventTouch = new AddEventTouchObject();
        blinkHandler = new BlinkHandler(currentIndex, touchObjects,pageController);
        spawnerTouchUI=new SpawnerTouchUI(existingTouches,pageController);
        AddEventTouch();
        AddObserver((ITouchObserver)searchText);
        AddObserver((ITouchObserver)touchUIHandler);
        AddObserver((ITouchObserver)blinkHandler);
        AddObserver((ITouchObserver)spawnerTouchUI);
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
    public  void LoadTexts()
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
    private void Update()
    {
        if (pageController != null)
        {
            blinkHandler.ShowFirstBlink(pageController);
        }
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


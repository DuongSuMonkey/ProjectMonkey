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
    #region
    //[SerializeField] private ITouchHandler touchHandler;
    //[SerializeField] private ISearchText searchText;
    //[SerializeField] private IAddEventTouchObject addEventTouch;
    //[SerializeField] private IBlinkHandler blinkHandler;
    //[SerializeField] private ITouchInitializer touchControllerInitializer;
    //[SerializeField] private ISpawnerTouchUI spawnerTouchUI;
    #endregion
    [SerializeField] private List<ITouchObserver> touchObservers = new List<ITouchObserver>();
    private ITouchUIController touchUIController;
    private ITouchHanleController hanleController;
    private IInitializerController initializerController;

    [SerializeField] private int currentIndex = 0;
    private void Start()
    {
        pageController = GetComponentInParent<IPageController>();
        #region
        //searchText = new SearchText(txtContents, this, touchObjects,pageController);
        //touchHandler = new TouchHandler(pageController);
        //addEventTouch = new AddEventTouchObject();
        //  blinkHandler = new BlinkHandler(currentIndex, touchObjects,pageController);
        //spawnerTouchUI=new SpawnerTouchUI(touchesUI,existingTouches,pageController);
        // AddEventTouch();
        //AddObserver((ITouchObserver)searchText);
        //AddObserver((ITouchObserver)touchHandler);
        //AddObserver((ITouchObserver)blinkHandler);
        //AddObserver((ITouchObserver)spawnerTouchUI);
        #endregion
        touchUIController = new TouchUIController(touchObjects, existingTouches, touchesUI, pageController, currentIndex);
        hanleController = new TouchHanleController(this, pageController, txtContents, touchObjects);
        initializerController = new InitializerController(pageController, touchObjects, HandleTouchSelection);
        AddObserver((ITouchObserver)hanleController);
        AddObserver((ITouchObserver)touchUIController);
    }
    private void Reset()
    {
        initializerController = new InitializerController(pageController, touchObjects, HandleTouchSelection);
       // touchControllerInitializer = new TouchInitializer();
        LoadComponents();
    }
    public void LoadComponents()
    {
        initializerController.LoadComponents(touchesUI, touchObjects, this);
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
    //public void AddEventTouch()
    //{
    //    addEventTouch.AddEventTouch(touchObjects, HandleTouchSelection);
    //}
    public void HandleTouchSelection(TouchObject touchObject)
    {
        NotifyObservers(touchObject);
    }
    private void Update()
    {
       touchUIController.ShowFirstBlink(pageController);
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


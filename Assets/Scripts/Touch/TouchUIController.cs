using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TouchUIController : ITouchUIController,ITouchObserver
{
    [SerializeField] private List<TouchObject> touchObjects;
    [SerializeField] private List<TouchUI> touchesUI;
    [SerializeField] private List<TouchUI> existingTouches;
    [SerializeField] private int currentIndex ;
    [SerializeField] private ISpawnerTouchUI spawnerTouchUI;
    [SerializeField] private IBlinkHandler blinkHandler;
    [SerializeField] private IPageController pageController;
    public TouchUIController(List<TouchObject> touchObjects, List<TouchUI> existingTouches, List<TouchUI> touchesUI, IPageController pageController, int currentIndex)
    {
        this.touchObjects = touchObjects;
        this.existingTouches = existingTouches;
        this.pageController = pageController;
        this.touchesUI = touchesUI;
        this.currentIndex = currentIndex;
        spawnerTouchUI = new SpawnerTouchUI(touchesUI, existingTouches, pageController);
        blinkHandler = new BlinkHandler(currentIndex, touchObjects, pageController);
    }
    public void Select(TouchObject touchObject)
    {
        ITouchObserver spawnerTouchUIObserver = spawnerTouchUI as ITouchObserver;
        spawnerTouchUIObserver.OnTouchSelected(touchObject);
        ITouchObserver blinkHandlerObserver = blinkHandler as ITouchObserver;
        blinkHandlerObserver.OnTouchSelected(touchObject);
    }
    public void OnTouchSelected(TouchObject touchObject)
    {
      Select(touchObject);
    }

    public void ShowFirstBlink(IPageController pageController)
    {
       blinkHandler.ShowFirstBlink(pageController);
    }
}

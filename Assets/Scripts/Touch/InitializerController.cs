using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializerController : IInitializerController
{
    [SerializeField] private IAddEventTouchObject addEventTouch;
    [SerializeField] private ITouchInitializer touchInitializer;
    [SerializeField] private IPageController pageController;
    public InitializerController(IPageController pageController,List<TouchObject> touchObjects, System.Action<TouchObject> onTouched)
    {
        touchInitializer = new TouchInitializer();
        this.pageController = pageController;
        addEventTouch=new AddEventTouchObject();
        addEventTouch.AddEventTouch(touchObjects, onTouched);
    }
    public void LoadComponents(List<TouchUI> touchesUI, List<TouchObject> touchObjects, MonoBehaviour obj)
    {
        touchInitializer.LoadComponents(touchesUI, touchObjects, obj);
    }
}

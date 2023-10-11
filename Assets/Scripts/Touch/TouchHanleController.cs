using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TouchHanleController : ITouchHanleController,ITouchObserver
{
    [SerializeField] private List<TextMeshProUGUI> txtContents;
    [SerializeField] private List<TouchObject> touchObjects;
    [SerializeField] private List<ITouchObserver> touchObservers = new List<ITouchObserver>();
    [SerializeField] private ITouchHandler touchHandler;
    [SerializeField] private ISearchText searchText;
    [SerializeField] private IPageController pageController;
    public TouchHanleController(MonoBehaviour obj,IPageController pageController,List<TextMeshProUGUI> txtContents, List<TouchObject> touchObjects)
    {
        this.pageController = pageController;
        this.txtContents=txtContents;
        this.touchObjects=touchObjects;
        touchHandler = new TouchHandler(pageController);
        searchText = new SearchText(txtContents, obj, touchObjects, pageController);
    }
    public void Select(TouchObject touchObject)
    {
        touchHandler.Select(touchObject);
        searchText.Search(touchObject);
    }
    public void OnTouchSelected(TouchObject touchObject)
    {
        Select(touchObject);
    }
}

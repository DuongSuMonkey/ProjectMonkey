using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : ITouchManager
{
    private List<TouchObject> touchObjects;
    private List<TouchUI> touchesUi;
    private int currentIndex;
    private List<TouchUI> existingTouches;
    private ITouchSelection touchSelection;
    private ITouchUIHandler touchUIHandler;
    private ISearchText searchText;
    public TouchManager(List<TouchUI> touchesUI, int currentIndex, List<TouchUI> existingTouches, List<TouchObject> touchObjects,ITouchUIHandler touchUIHandler)
    {
        this.touchesUi = touchesUI;
        this.currentIndex = currentIndex;
        this.existingTouches = existingTouches;
        this.touchObjects = touchObjects;
        searchText = new SearchTextController();
        touchSelection =new TouchSelection(existingTouches, touchObjects,touchesUI);
        this.touchUIHandler= touchUIHandler;
    }
    
    [System.Obsolete]
    public void LoadTouchObjects(List<TouchObject> touchObjects,MonoBehaviour obj)
    {
        var touchObjectsArray = obj.GetComponentsInChildren<TouchObject>();
        for (int i = touchObjectsArray.Length - 1; i >= 0; i--)
        {
            touchObjects.Add(touchObjectsArray[i]);
        }
    }

    public void SelectTouchObject(TouchObject touchObject, int index,IPageController pageController)
    {
        if (!pageController.IsFinal())
        {
            return;
        }
        touchSelection.SelectTouchObject(touchUIHandler, touchObjects, touchObject, index);
    }

    public void AddEventTouch(List<TouchObject> touchObjects, System.Action<TouchObject> onTouched)
    {
        foreach (var touchObject in touchObjects)
        {
            touchObject.OnClicked += onTouched;
        }
    }

}

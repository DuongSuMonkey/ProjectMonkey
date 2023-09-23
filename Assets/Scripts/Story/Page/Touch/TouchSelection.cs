using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSelection :ITouchSelection
{
    private List<TouchObject> touchObjects;
    private List<TouchUI> existingTouches;
    private List<TouchUI> touchesUI;
    public TouchSelection(List<TouchUI> existingTouches,List<TouchObject>touchObjects,List<TouchUI>touchesUI)
    {
        this.existingTouches = existingTouches;
        this.touchObjects = touchObjects;
        this.touchesUI = touchesUI;
    }
    public void SelectTouchUI(TouchUI touchUI)
    {
        existingTouches.Add(touchUI);
        touchUI.Select();
    }
    public void SelectTouchObject(ITouchUIHandler touchUIHandler,List<TouchObject> touchObjects, TouchObject touchObject, int index)
    {
        touchObject.Select(touchUIHandler, touchObjects, touchesUI, index);
    }
}

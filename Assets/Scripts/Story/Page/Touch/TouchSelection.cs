using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSelection :ITouchSelection
{
    private List<TouchUI> existingTouches = new List<TouchUI>();
    public TouchSelection(List<TouchUI> existingTouches)
    {
        this.existingTouches = existingTouches;
    }
    public void SelectTouchUI(TouchUI touchUI)
    {
        existingTouches.Add(touchUI);
        touchUI.Select();
    }
}

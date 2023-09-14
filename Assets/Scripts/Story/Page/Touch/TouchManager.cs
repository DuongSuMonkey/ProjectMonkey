using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : ITouchManager,ITouchSelection
{
    private List<TouchUI> touchesUi;
    private int currentIndex;
    private List<TouchUI> existingTouches;
    private ITouchSelection touchSelection;
    public TouchManager(List<TouchUI> touchesUI, int currentIndex, List<TouchUI> existingTouches)
    {
        this.touchesUi = touchesUI;
        this.currentIndex = currentIndex;
        this.existingTouches = existingTouches;
        touchSelection = new TouchSelection(existingTouches);
    }
    
    public void CreateTouch(TouchObject touchObject,int index)
    {
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TouchUI touch = Object.Instantiate(touchesUi[index], new Vector3(canvasPos.x, canvasPos.y,0),
           Quaternion.Euler(new Vector3(0,0,Random.Range(-15,15))));
        touch.gameObject.transform.SetParent(touchObject.transform);
        touch.gameObject.transform.localScale = Vector3.one;
        AddTouchExisting(touch);
        SelectTouchUI(touch);
        touch.DestroyTouchCoroutine();
    }
    public void HideAllTouch()
    {
        foreach (TouchUI touch in existingTouches)
        {
            if (touch != null)
            {
                touch.HideTouch();
            }
        }
    }
    public void AddTouchExisting(TouchUI touch)
    {
        existingTouches.Add(touch);
    }
    public void SelectTouchUI(TouchUI touchUI)
    {
        touchUI.Select();
    }
}

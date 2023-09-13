using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : ITouchManager
{
    private List<TouchUI> touches;
    private int currentIndex;
    private List<TouchUI> existingTouches;
    public TouchManager(List<TouchUI> touches, int currentIndex, List<TouchUI> existingTouches)
    {
        this.touches = touches;
        this.currentIndex = currentIndex;
        this.existingTouches = existingTouches;
    }
    
    public void CreateTouch(Blink blink, int index)
    {
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TouchUI touch = Object.Instantiate(touches[index], new Vector3(canvasPos.x, canvasPos.y, 0), Quaternion.identity);
        touch.gameObject.transform.SetParent(blink.transform);
        touch.gameObject.transform.localScale = Vector3.one;
        existingTouches.Add(touch);
        touch.Select();
        touch.DestroyTouchCoroutine();
    }

    public void HideAlTouch(List<TouchUI> existingTouches)
    {
        foreach (TouchUI touch in existingTouches)
        {
            if (touch != null)
            {
                touch.HideTouch();
            }
        }
    }
}

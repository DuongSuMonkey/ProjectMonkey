using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkController:IBlinkController
{
    private List<Blink> blinks;
    private List<TouchUI> touches;

    public BlinkController(List<Blink> blinks, List<TouchUI> touches)
    {
        this.blinks = blinks;
        this.touches = touches;
    }

    public void ProcessDoubleClick(Blink blink, int index)
    {
        blink.gameObject.GetComponent<Image>().enabled = false;
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TouchUI touch = Object.Instantiate(touches[index], new Vector3(canvasPos.x, canvasPos.y, 0), Quaternion.identity);
        touch.gameObject.transform.SetParent(blink.transform);
        touch.gameObject.transform.localScale = Vector3.one;
        touch.Select();
        touch.GetDestroy();
    }
}


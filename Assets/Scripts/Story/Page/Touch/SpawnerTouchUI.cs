using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTouchUI : ISpawnerTouchUI
{
    List<TouchUI> existingTouches;
    public SpawnerTouchUI(List<TouchUI> existingTouches)
    {
        this.existingTouches = existingTouches;
    }

    public void SpamTouchUI(List<TouchUI> touchesUI, TouchObject touchObject)
    {
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TouchUI touch = UnityEngine.Object.Instantiate(touchObject.touchUI, new Vector3(canvasPos.x, canvasPos.y, 0),
         Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(-15, 15))));
        touch.gameObject.transform.SetParent(touchObject.transform);
        touch.gameObject.transform.localScale = Vector3.one;
        existingTouches.Add(touch);
        touch.DestroyTouchCoroutine();
        touch.Select();
    }
}

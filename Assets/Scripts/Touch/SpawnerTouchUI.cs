using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTouchUI : ISpawnerTouchUI,ITouchObserver
{

    private List<TouchUI> touchesUI;
    private List<TouchUI> existingTouches;
    private IPageController pageController;
    public SpawnerTouchUI(List<TouchUI> touchesUI,List<TouchUI> existingTouches, IPageController pageController)
    {
        this.existingTouches = existingTouches;
        this.pageController = pageController;
        this.touchesUI = touchesUI;
        HideAllTouchesUI(this.touchesUI);
    }
    public void OnTouchSelected(TouchObject touchObject)
    {
        SpamTouchUI(touchObject);
            
    }

    public void SpamTouchUI(TouchObject touchObject)
    {
        if (pageController.IsSyncFinish())
        {
            HideAllexistingTouchesUI();
            Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TouchUI touch = UnityEngine.Object.Instantiate(touchObject.touchUI, new Vector3(canvasPos.x, canvasPos.y, 0),
             Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(-15, 15))));
            touch.gameObject.transform.SetParent(touchObject.transform);
            touch.gameObject.transform.localScale = Vector3.one;
            existingTouches.Add(touch);
            touch.Select();
            touch.DestroyTouchCoroutine();
        }
    }
    public void HideAllexistingTouchesUI()
    {
        foreach (TouchUI touch in existingTouches)
        {
            if (touch != null)
            {
                touch.HideTouch();
            }
        }
    }
    public void HideAllTouchesUI(List<TouchUI> touchesUI)
    {
        foreach (var touch in touchesUI)
        {
            touch.HideTouch();
        }
    }

}

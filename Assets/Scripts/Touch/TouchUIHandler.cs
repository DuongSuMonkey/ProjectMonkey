using System.Collections.Generic;
using Unity.VisualScripting;

public class TouchUIHandler:ITouchUIHandler,ITouchObserver
{
    private List<TouchUI> touchesUI;
    private List<TouchUI> existingTouches;
    private IPageController pageController;
    public TouchUIHandler(List<TouchUI> touchesUI,List<TouchUI> existingTouches,IPageController pageController)
    {
        this.touchesUI = touchesUI;
        this.pageController = pageController;
        HideAllTouchesUI(touchesUI);
    }
    public void Select(TouchObject touchObject)
    {
        if (pageController.IsSyncFinish())
        {
            touchObject.Select();
        }
    }

    public void HideAllTouchesUI(List<TouchUI> touchesUI)
    {
        foreach (var touch in touchesUI)
        {
            touch.HideTouch();
        }
    }

    public void OnTouchSelected(TouchObject touchObject)
    {
        Select(touchObject);
    }
}


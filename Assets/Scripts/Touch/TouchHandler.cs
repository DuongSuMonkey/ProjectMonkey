using System.Collections.Generic;
using Unity.VisualScripting;

public class TouchHandler:ITouchHandler,ITouchObserver
{

    private IPageController pageController;
    public TouchHandler(IPageController pageController)
    {
        this.pageController = pageController;
    }
    public void Select(TouchObject touchObject)
    {
        if (pageController.IsSyncFinish())
        {
            touchObject.Select();
        }
    }

    public void OnTouchSelected(TouchObject touchObject)
    {
        Select(touchObject);
    }

}


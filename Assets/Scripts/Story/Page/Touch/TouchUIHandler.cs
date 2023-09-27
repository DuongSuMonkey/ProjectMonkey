using System.Collections.Generic;

public class TouchUIHandler:ITouchUIHandler, ITouchObserver
{
    private List<TouchObject> touchObjects;
    private List<TouchUI> touchesUI;
    private int currentIndex;
    private List<TouchUI> existingTouches;
    private ISpawnerTouchUI SpawnerTouchUI;
    public TouchUIHandler(List<TouchUI> touchesUI, int currentIndex, List<TouchUI> existingTouches, List<TouchObject> touchObjects)
    {
        this.touchesUI = touchesUI;
        this.currentIndex = currentIndex;
        this.existingTouches = existingTouches;
        this.touchObjects = touchObjects;
        SpawnerTouchUI = new SpawnerTouchUI(existingTouches);
        HideAllTouchesUI(touchesUI);
    }
    public void ShowTouchUICurrent()
    {
        if (!IsProcessingRemaining())
        {
            return;
        }
        SpawnTouchUI(touchObjects[currentIndex]);
        IncreaseCurrentIndex();
    }
    public void Select(TouchObject touchObject)
    {
        touchObject.Select();
        HideAllexistingTouchesUI();
        if (touchObject.countClick > 1 || touchObject.touchUI != GetTouch())
        {
            ProcessDoubleClick(touchObject);
            return;
        }
        ShowTouchUICurrent();
    }
    public bool IsProcessingRemaining()
    {
       return currentIndex<touchObjects.Count;
    }
    public void IncreaseCurrentIndex()
    {
        if (CanIncreaseCurrentIndex())
        {
            if (touchObjects[currentIndex].isClick || !touchObjects[currentIndex].hasBlink)
            {
                currentIndex++;
                IncreaseCurrentIndex();

            }
        }
    }
    public bool CanIncreaseCurrentIndex()
    {
         return currentIndex<touchObjects.Count-1;
    }
    public void ProcessDoubleClick(TouchObject touchObject)
    {
        HideAllexistingTouchesUI();
        SpawnTouchUI(touchObject );
    }
    public TouchUI GetTouch()
    {
        return touchesUI[currentIndex];
    }
    public void SpawnTouchUI(TouchObject touchObject)
    {
        SpawnerTouchUI.SpamTouchUI(touchesUI,touchObject);
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

    public void OnTouchSelected(TouchObject touchObject)
    {
        Select(touchObject);
    }
}


using System.Collections.Generic;

public class TouchUIHandler:ITouchUIHandler
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
        SpawnTouchUI(touchObjects[currentIndex], currentIndex);
        IncreaseCurrentIndex();
    }
    public void Select(TouchObject touchObject,int index)
    {
        touchObject.Select();
        HideAllexistingTouchesUI();
        if (touchObject.countClick > 1 || touchesUI[index] != GetTouch())
        {
            ProcessDoubleClick(touchObject, index);
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
    public void ProcessDoubleClick(TouchObject touchObject, int index)
    {
        HideAllexistingTouchesUI();
        SpawnTouchUI(touchObject ,index);
    }
    public TouchUI GetTouch()
    {
        return touchesUI[currentIndex];
    }
    public void SpawnTouchUI(TouchObject touchObject, int index)
    {
        SpawnerTouchUI.SpamTouchUI(touchesUI,touchObject,index);
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


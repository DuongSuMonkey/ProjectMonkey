using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ITouchUIHandler
{
    void ShowBlinkNext(List<TouchObject> touchObjects);
    void ShowTouchCurrent(List<TouchObject> touchObjects, List<TouchUI> touches);
    void ProcessDoubleClick(TouchObject touchObject, int index);
    bool IsProcessingRemaining(List<TouchObject> touchObjects);
    TouchUI GetTouch();
    void HideAllTouch();
    void HideAllTouchesUI(List<TouchUI> touchesUI);
}

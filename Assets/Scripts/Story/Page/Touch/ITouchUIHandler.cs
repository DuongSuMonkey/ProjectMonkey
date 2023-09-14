using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ITouchUIHandler
{
    void ShowBlinkNext(List<TouchObject> touchObjects);
    void ShowTouchCurrent(List<TouchObject> touchObjects, List<TouchUI> touches);
    void ProcessDoubleClick(TouchObject touchObject, int index);
    TouchUI GetTouch();
    void HideAllTouch();

}

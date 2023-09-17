using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlinkController 
{
    TouchObject GetTouchObject(TouchObject touchObject);
    void HideBlinkEffect(TouchObject touchObject);
    void ShowBlinkEffect(TouchObject touchObject);
    bool IsProcessingRemaining(List<TouchObject> touchObjects);
    bool CanNextBlink(List<TouchObject> touchObjects);
    void ShowBlink(List<TouchObject> touchObject);
    void CanBlink(List<TouchObject> touchObjects);
}

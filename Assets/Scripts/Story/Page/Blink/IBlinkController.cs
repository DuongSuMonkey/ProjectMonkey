using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlinkController 
{
    void HideBlinkEffect(TouchObject touchObject);
    void ShowBlinkEffect(TouchObject touchObject);
    bool IsProcessingRemaining(int currentIndex , List<TouchObject> touchObjects);
    bool CanNextBlink(int currentIndex,List<TouchObject> touchObjects);
    void ShowBlink(List<TouchObject> touchObject);
    void CanBlink(List<TouchObject> touchObjects);
    void ShowFirstBlink();
    void HideAllBlinks(List<TouchObject> touchObjects);
    void UpdateFirstBlink(IPageController pageController);
}

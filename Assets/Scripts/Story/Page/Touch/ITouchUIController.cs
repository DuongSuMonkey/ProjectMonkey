using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ITouchUIController
{
    void ShowTouchNext(List<Blink> blinks);
    void ShowTouchCurrent(List<Blink> blinks, List<TouchUI> touches,MonoBehaviour obj);
    bool IsNextTouch(List<Blink> blinks);
    bool IsTouchFinal(List<Blink> blinks);
    void ProcessDoubleClick(Blink blink, int index);
    TouchUI GetTouch();
  
}

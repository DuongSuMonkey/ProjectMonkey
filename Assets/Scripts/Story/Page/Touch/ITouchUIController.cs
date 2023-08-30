using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ITouchUIController
{
    void ShowTouchNext(List<Blink> blinks);
    void ShowTouchCurrent(List<Blink> blinks, List<TouchUI> touches,MonoBehaviour obj);
    void SearchText(TouchUI touch, List<TextMeshProUGUI> txtsContent);
    bool IsTouchFinal(List<Blink> blinks);
    bool IsNextTouch(List<Blink> blinks);
}

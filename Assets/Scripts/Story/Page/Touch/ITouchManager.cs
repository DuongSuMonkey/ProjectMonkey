using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchManager
{
    void CreateTouch(Blink blink, int index);
    void HideAlTouch(List<TouchUI> existingTouches);
}

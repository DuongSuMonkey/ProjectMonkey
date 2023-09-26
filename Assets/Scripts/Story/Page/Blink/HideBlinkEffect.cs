using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBlinkEffect : IHideBlinkEffect
{
    public void HideAllBlinks(List<TouchObject> touchObjects)
    {
        foreach (var touchObject in touchObjects)
        {
            HideBlink(touchObject);
        }
    }

    public void HideBlink(TouchObject touchObject)
    {
        touchObject.blinkEffect.gameObject.SetActive(false);
    }
}

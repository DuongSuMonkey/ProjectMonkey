using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkController:IBlinkController
{
    private List<TouchUI> touches;

    public BlinkController(List<TouchUI> touches)
    {
        this.touches = touches;
    }

    public Blink GetBlink(Blink blink)
    {
        return blink;
    }

    public void HideBlinkEffect(Blink blink)
    {
        blink.blinkEffect.gameObject.SetActive(false);
    }

    public void ShowBlinkEffect(Blink blink)
    {
        blink.blinkEffect.gameObject.SetActive(true);
    }
}


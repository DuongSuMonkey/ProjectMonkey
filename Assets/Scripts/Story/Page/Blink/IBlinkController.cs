using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlinkController 
{
    Blink GetBlink(Blink blink);
    void HideBlinkEffect(Blink blink);
    void ShowBlinkEffect(Blink blink);
}

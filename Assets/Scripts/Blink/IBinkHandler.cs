using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlinkHandler
{
    void ShowFirstBlink(IPageController pageController);
    void Select();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlinkHandler: ITouchObserver
{
    void ShowFirstBlink(IPageController pageController);
    void Select();
}

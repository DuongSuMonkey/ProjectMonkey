using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlinkHandler 
{
    void GetFirstBlink();
    void ShowFirstBlink(IPageController pageController);
    void Select();
}

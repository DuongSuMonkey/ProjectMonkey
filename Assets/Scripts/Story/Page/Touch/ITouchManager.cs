using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchManager
{
   // void LoadTouchObjects(List<TouchObject> touchObjects, MonoBehaviour obj);
    void AddEventTouch(List<TouchObject> touchObjects, System.Action<TouchObject> onTouched);
    void SelectTouchObject(TouchObject touchObject, int index,IPageController pageController);
}

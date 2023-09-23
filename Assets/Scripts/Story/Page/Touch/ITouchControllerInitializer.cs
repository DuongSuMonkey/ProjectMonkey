using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchControllerInitializer 
{
    void LoadTouchesUI(List<TouchUI> touchesUI, List<TouchObject> touchObjects);
    void SetAnchors(List<TouchObject> touchObjects);
    void SetPosition(List<TouchObject> touchObjects);
    void SetScale(List<TouchObject> touchObjects);
    void HideAllTouchesUI(List<TouchUI> touchObjects);
   // void LoadPageController(MonoBehaviour obj, PageController pageController);
    void LoadComponents(List<TouchUI> touchesUI, List<TouchObject> touchObjects, MonoBehaviour obj);
}

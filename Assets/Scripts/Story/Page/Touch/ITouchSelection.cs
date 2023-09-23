using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchSelection 
{
    void SelectTouchUI(TouchUI touchUI);
    void SelectTouchObject(ITouchUIHandler touchUIHandler, List<TouchObject> touchObjects, TouchObject touchObject, int index);
}

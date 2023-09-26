using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHideBlinkEffect 
{
    public void HideBlink(TouchObject touchObject);
    void HideAllBlinks(List<TouchObject> touchObjects);
}

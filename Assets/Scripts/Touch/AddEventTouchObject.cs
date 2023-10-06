using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEventTouchObject : IAddEventTouchObject
{
    public AddEventTouchObject()
    {

    }
    

    public void AddEventTouch(List<TouchObject> touchObjects, System.Action<TouchObject> onTouched)
    {
        foreach (var touchObject in touchObjects)
        {
            touchObject.OnClicked += onTouched;
        }
    }
}

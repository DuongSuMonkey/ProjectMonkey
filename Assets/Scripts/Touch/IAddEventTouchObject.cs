using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddEventTouchObject
{
    void AddEventTouch(List<TouchObject> touchObjects, System.Action<TouchObject> onTouched);
}

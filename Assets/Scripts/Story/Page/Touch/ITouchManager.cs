using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchManager
{
    void CreateTouch(TouchObject touchObject, int index);
    void HideAllTouch();
}

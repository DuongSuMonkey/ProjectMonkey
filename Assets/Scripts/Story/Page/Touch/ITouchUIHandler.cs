using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ITouchUIHandler:ITouchObserver
{
    void Select(TouchObject touchObject);
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ISearchText: ITouchObserver
{
    void Search(TouchObject touchObject);
}

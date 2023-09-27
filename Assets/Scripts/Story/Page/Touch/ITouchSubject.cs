using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchSubject 
{
    void AddObserver(ITouchObserver observer);
    void RemoveObserver(ITouchObserver observer);
    void NotifyObservers(TouchObject touchObject);
}

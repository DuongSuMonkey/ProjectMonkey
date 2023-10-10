using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchInitializer 
{ 
    void LoadComponents(List<TouchUI> touchesUI, List<TouchObject> touchObjects, MonoBehaviour obj);
}

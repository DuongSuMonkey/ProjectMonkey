using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchControllerInitializer 
{ 
    void LoadComponents(List<TouchUI> touchesUI, List<TouchObject> touchObjects, MonoBehaviour obj);
}

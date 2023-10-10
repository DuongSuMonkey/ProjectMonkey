using System.Collections.Generic;
using UnityEngine;

public interface IInitializerController
{
    void LoadComponents(List<TouchUI> touchesUI, List<TouchObject> touchObjects, MonoBehaviour obj);
}
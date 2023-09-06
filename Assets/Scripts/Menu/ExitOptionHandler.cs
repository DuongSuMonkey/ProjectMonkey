using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOptionHandler : MonoBehaviour, IMenuOptionHandler
{
    public void HandleOption()
    {
        Debug.Log("Exit");
    }
}

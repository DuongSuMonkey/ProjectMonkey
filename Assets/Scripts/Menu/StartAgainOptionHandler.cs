using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAgainOptionHandler : MonoBehaviour, IMenuOptionHandler
{
    public void HandleOption()
    {
        Debug.Log("Start Again");
    }
}

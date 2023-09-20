using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOptionHandler :IMenuOptionHandler
{
    public void HandleOption()
    {
        Debug.Log("Exit");
    }
}

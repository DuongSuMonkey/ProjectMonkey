using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISyncTextColor 
{
    void TextColorSync(MonoBehaviour obj, ITimeSync timeSync);
    void Reload();
    bool IsFinish();
}

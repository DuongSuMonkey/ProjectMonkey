using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIsProcessingRemaining
{
    bool IsProcessingRemaining(int currentIndex, List<TouchObject> touchObjects);
}

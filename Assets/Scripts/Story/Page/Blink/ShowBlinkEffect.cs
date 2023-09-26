using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBlinkEffect : IShowBlinkEffect
{
    public void ShowBlink(int currentIndex,List<TouchObject> touchObjects)
    {
        CanBlink(currentIndex,touchObjects);
        if (touchObjects[currentIndex].hasBlink)
        {
            touchObjects[currentIndex].blinkEffect.gameObject.SetActive(true);
        }
    }
    public void CanBlink(int currentIndex,List<TouchObject> touchObjects)
    {
        if (currentIndex<touchObjects.Count-1)
        {
            if (touchObjects[currentIndex].isClick || !touchObjects[currentIndex].hasBlink)
            {
                currentIndex++;
                CanBlink(currentIndex, touchObjects);
            }
        }
    }
  
}

using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncTextColor : ISyncTextColor
{
    private List<TextMeshProUGUI> txtContents;
    private Color targetColor;
    private bool isFinish;
    private int currentIndex;

    public SyncTextColor(List<TextMeshProUGUI> txtContents, Color targetColor)
    {
        this.txtContents = txtContents;
        this.targetColor = targetColor;
        this.currentIndex = 1;
        isFinish = false;
    }

    public void TextColorSync(MonoBehaviour obj,ITimeSync timeSync)
    {
        if (!isFinish)
        {
            timeSync.Sync();
            if (timeSync.CanSync())
            {
                foreach (var txtContent in txtContents)
                {
                    txtContent.color = Color.black;
                }

                txtContents[currentIndex].color = targetColor;
                currentIndex++;
                timeSync.Reset();
            }
            else if (currentIndex == txtContents.Count)
            {
                obj.StartCoroutine(SyncFinalTextColor(timeSync));
            }
        }
    }
    IEnumerator SyncFinalTextColor(ITimeSync timeSync) {
        yield return new WaitForSeconds(timeSync.TimeSyncFinal());
        txtContents[currentIndex - 1].color = Color.black;
        isFinish = true;
    }

    public void Reload()
    {
        isFinish= false;
        currentIndex = 1;
        txtContents[0].color = targetColor;
    }

    public bool IsFinish()
    {
       return this.isFinish;
    }
}

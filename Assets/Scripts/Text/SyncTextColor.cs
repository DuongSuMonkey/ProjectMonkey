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
    private ITimer time;

    public SyncTextColor(List<TextMeshProUGUI> txtContents, Color targetColor,
        List<SyncData> syncData)
    {
        this.txtContents = txtContents;
        this.targetColor = targetColor;
        this.currentIndex = 1;
        time = new Timer(txtContents, syncData);
        isFinish = false;
    }

    public void TextColorSync(MonoBehaviour obj)
    {
        time.Sync();
        time.UpdateTimeSync();
        if (time.CanSync())
        {
            foreach (var txtContent in txtContents)
            {
                txtContent.color = Color.black;
            }

            txtContents[currentIndex].color = targetColor;
            currentIndex++;
            time.IncreateIndex();
            time.ResetTime();
        }
        else if (currentIndex == txtContents.Count && !isFinish)
        {
            obj.StartCoroutine(SyncFinalTextColor());
        }
    }
    IEnumerator SyncFinalTextColor() {
        yield return new WaitForSeconds(time.TimeSyncFinal());
        txtContents[currentIndex - 1].color = Color.black;
        isFinish = true;
    }

    public void Reload()
    {
        isFinish= false;
        currentIndex = 1;
        time.Reload();
        txtContents[0].color = targetColor;
    }

    public bool IsFinish()
    {
       return this.isFinish;
    }
}

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
    private float timeChange;
    private float timer;
    private int syncDataIndex = 0;
    private int currentIndex;
    private List<SyncData> syncData;

    public SyncTextColor(List<TextMeshProUGUI> txtContents, Color targetColor,
        List<SyncData> syncData,int currentIndex, float timer, float timeChange)
    {
        this.txtContents = txtContents;
        this.targetColor = targetColor;
        this.syncData = syncData;
        this.currentIndex = currentIndex;
        this.timer = timer;
        isFinish = false;
    }

    public void TextColorSync(MonoBehaviour obj)
    {
        TimerSync();
        UpdateTimeSync();
        if (timer >= timeChange && currentIndex < txtContents.Count)
        {
            foreach (var txtContent in txtContents)
            {
                txtContent.color = Color.black;
            }

            txtContents[currentIndex].color = targetColor;
            currentIndex++;
            syncDataIndex++;
            timer = 0.0f;
        }
        else if (currentIndex == txtContents.Count && !isFinish)
        {
            timeChange = syncData[txtContents.Count - 1].timeEnd / 1000 - syncData[txtContents.Count - 1].timeStart / 1000;
            obj.Invoke(nameof(SyncFinalTextColor),timeChange);
        }
    }

    public void SyncFinalTextColor()
    {
        txtContents[currentIndex-1].color = Color.black;
        isFinish = true;
    }
    public void TimerSync()
    {
        if (currentIndex < txtContents.Count)
        {
            timer += Time.deltaTime;
        }
    }
    protected void UpdateTimeSync()
    {
        if (currentIndex < txtContents.Count)
        {
            timeChange = syncData[syncDataIndex].timeEnd / 1000 - syncData[syncDataIndex].timeStart / 1000;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSync :ITimeSync
{

    private float timer;
    private List<TextMeshProUGUI> txtContents;
    private int currentIndex;
    private float timeSync;
    private List<SyncData> syncData;

    public TimeSync(List<TextMeshProUGUI> txtContents, List<SyncData> syncData)
    {
        
        this.timer = 0;
        this.txtContents = txtContents;
        this.syncData = syncData;
        this.currentIndex = 1;
        this.timeSync = syncData[0].timeEnd / 1000 - syncData[0].timeStart / 1000;
    }


    public void Sync()
    {
        if (currentIndex < txtContents.Count)
        {
            timer += Time.deltaTime;
            timeSync = syncData[currentIndex - 1].timeEnd / 1000 - syncData[currentIndex - 1].timeStart / 1000;
        }
    }

    public void Reset()
    {
        currentIndex++;
        timer = 0.0f;
    }


    public bool CanSync()
    {
        return timer >= timeSync && currentIndex < txtContents.Count;
    }
    public float TimeSyncFinal()
    {
        return syncData[txtContents.Count - 1].timeEnd / 1000 - syncData[txtContents.Count - 1].timeStart / 1000;
    }

    public void Reload()
    {
        currentIndex = 1;
        timer = 0.0f;
        timeSync = syncData[0].timeEnd / 1000 - syncData[0].timeStart / 1000;
    }
}

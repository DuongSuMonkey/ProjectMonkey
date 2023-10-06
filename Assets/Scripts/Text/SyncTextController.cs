using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncTextController :Texts, ISyncTextController
{
    [SerializeField] private Color targetColor=Color.red;
    [SerializeField] private bool isFinish=false;
    [SerializeField] private float timeChange;
    [SerializeField] private float timer;
    [SerializeField] private int syncDataIndex = 0;
    [SerializeField] public bool IsFinish { get => isFinish;}
    [SerializeField] public List<SyncData> syncData;
    [SerializeField] private ISyncTextColor syncTextColor;
    void Start()
    {
        timeChange = syncData[syncDataIndex].timeEnd / 1000 - syncData[syncDataIndex].timeStart / 1000;
        txtContents[0].color = targetColor;
        currentIndex = 1;
        syncTextColor = new SyncTextColor(txtContents, targetColor, syncData, currentIndex, timer, timeChange);
    }
    private void Reset()
    {
        LoadComponents();
    }
    public void LoadComponents()
    {
       LoadTexts();
    }
    public override void LoadTexts()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        txtContents.AddRange(texts);
    }
    private void Update()
    {
        //  TimerSync();
            TextColorSync();
    }
    public void Reload()
    {
        syncTextColor.Reload();
        isFinish = false;
    }
    public void SyncFinalTextColor()
    {
        syncTextColor.SyncFinalTextColor();
       // txtContents[txtContents.Count - 1].color = Color.black;
        this.isFinish = true;
    }
    //public void TimerSync()
    //{
    //    if (currentIndex < txtContents.Count)
    //    {
    //        timer += Time.deltaTime;
    //    }
    //}
    public void TextColorSync()
    {
        syncTextColor.TextColorSync(this);
        #region
        //UpdateTimeSync();
        //if (timer >= timeChange && currentIndex < txtContents.Count)
        //{
        //    foreach (var txtContent in txtContents)
        //    {
        //        txtContent.color = Color.black;
        //    }
        //    txtContents[currentIndex].color = targetColor;
        //    currentIndex++;
        //    syncDataIndex++;
        //    timer = 0.0f;
        //}
        //else if (currentIndex == txtContents.Count && !isFinish)
        //{
        //    Invoke(nameof(SyncFinalTextColor), syncData[txtContents.Count - 1].timeEnd / 1000 - syncData[txtContents.Count - 1].timeStart / 1000);
        //}
        #endregion
    }

    public bool IsFinishSync()
    {
       return this.isFinish;
    }
    //protected void UpdateTimeSync()
    //{
    //    if (currentIndex < txtContents.Count)
    //    {
    //        timeChange = syncData[syncDataIndex].timeEnd / 1000 - syncData[syncDataIndex].timeStart / 1000;
    //    }
    //}
}


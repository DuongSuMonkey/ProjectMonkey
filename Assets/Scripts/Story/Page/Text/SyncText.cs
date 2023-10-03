using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncText :Texts, ISyncText
{
    [SerializeField] private Color targetColor=Color.red;
    [SerializeField] private bool isFinish=false;
    [SerializeField] protected float timeChange;
    [SerializeField] protected float timer;
    public int syncDataIndex = 0;
    public bool IsFinish { get => isFinish;}
    public List<SyncData> syncData;
    void Start()
    {
        timeChange = syncData[syncDataIndex].timeEnd / 1000 - syncData[syncDataIndex].timeStart / 1000;
        txtContents[0].color = targetColor;
        currentIndex = 1;
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
        ChangeTime();
        ChangeColor();
    }
    public void ChangeTextColorFinal()
    {
        txtContents[currentIndex - 1].color = Color.black;
        isFinish = true;
    }
    public void ChangeTime()
    {
        if (currentIndex < txtContents.Count)
        {
            timer += Time.deltaTime;
        }
    }
    public void ChangeColor()
    {
        if (currentIndex < txtContents.Count)
        {
            timeChange = syncData[syncDataIndex].timeEnd / 1000 - syncData[syncDataIndex].timeStart / 1000;
        }
        if (timer >= timeChange && currentIndex < txtContents.Count)
        {
            foreach(var txtContent in txtContents)
            {
                txtContent.color = Color.black;
            }
            txtContents[currentIndex].color = targetColor;
            currentIndex++;
            syncDataIndex++;
            timer = 0.0f;
        }
        else if (currentIndex == txtContents.Count  && !isFinish)
        {
            Invoke(nameof(ChangeTextColorFinal), syncData[txtContents.Count - 1].timeEnd / 1000 - syncData[txtContents.Count - 1].timeStart / 1000);
        }
    }
}


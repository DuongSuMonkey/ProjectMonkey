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
    void Awake()
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
         TextColorSync();
    }
    public void Reload()
    {
        if (syncTextColor != null)
        {
            syncTextColor.Reload();
            isFinish = false;
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = true;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
    public void SyncFinalTextColor()
    {
        syncTextColor.SyncFinalTextColor();
        this.isFinish = true;
    }
    public void TextColorSync()
    {
        syncTextColor.TextColorSync(this);
    }

    public bool IsFinishSync()
    {
       return this.isFinish;
    }
}


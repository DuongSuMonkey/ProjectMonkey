using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncTextController :MonoBehaviour, ISyncTextController
{
    [SerializeField] private Color targetColor=Color.red;
    [SerializeField] public List<SyncData> syncData;
    [SerializeField] private ISyncTextColor syncTextColor;
    [SerializeField] private ITimeSync timeSync;
    [SerializeField] public List<TextMeshProUGUI> txtContents;
    void Awake()
    {
        syncTextColor = new SyncTextColor(txtContents, targetColor);
        timeSync = new TimeSync(txtContents, syncData);
    }
    private void Reset()
    {
        LoadComponents();
    }
    public void LoadComponents()
    {
       LoadTexts();
    }
    public  void LoadTexts()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        txtContents.AddRange(texts);
    }
    private void Update()
    {
        syncTextColor.TextColorSync(this,timeSync);
    }
    public void Reload()
    {
        syncTextColor.Reload();
        timeSync.Reload();
        ReloadAudio();
    }
    public void ReloadAudio()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.PlayOneShot(audioSource.clip);
    }

    public bool IsFinishSync()
    {
        return syncTextColor.IsFinish();
    }
}


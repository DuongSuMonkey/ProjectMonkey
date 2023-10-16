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
    [SerializeField] public List<TextMeshProUGUI> txtContents;
    void Awake()
    {
        syncTextColor = new SyncTextColor(txtContents, targetColor, syncData);
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
        syncTextColor.TextColorSync(this);
    }
    public void Reload()
    {
        if (syncTextColor != null)
        {
            syncTextColor.Reload();
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = true;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    public bool IsFinishSync()
    {
        return syncTextColor.IsFinish();
    }
}


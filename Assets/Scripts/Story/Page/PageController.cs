using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageController : MonoBehaviour, IPageController
{
    [SerializeField] private List<SyncText> syncTexts;
    [SerializeField] protected TouchManager touchControllers;
    [SerializeField] protected List<AudioSource> audioSource;
    [SerializeField] private List<AudioClip> audioClip;
    public List<SyncText> SyncText { get => syncTexts; }
    public List<AudioClip> AudioClip { get => audioClip;}

    private void Start()
    {
        HideAllSyncText();
        ShowFirstSyncText();
    }
    private void Reset()
    {
        LoadComponents();
    }
    private void Update()
    {
        for (int i = 0; i < syncTexts.Count; i++)
        {
            if (i < syncTexts.Count - 1 && syncTexts[i].IsFinish)
            {
                syncTexts[i].gameObject.SetActive(false);
                syncTexts[++i].gameObject.SetActive(true);
            }
        }
    }
    public void LoadComponents()
    {
        LoadTouchController();
        LoadSyncText();
        LoadAudios();
        LoadAudioClips();
    }
    public void LoadTouchController()
    {
        touchControllers = GetComponentInChildren<TouchManager>();
    }
    private void LoadSyncText()
    {
        SyncText[] texts = GetComponentsInChildren<SyncText>();
        syncTexts.AddRange(texts);
    }
    private void LoadAudios()
    {
        for (int i = 0; i < syncTexts.Count; i++)
        {
            AudioSource audio = SyncText[i].GetComponent<AudioSource>();
            audioSource.Add(audio);
        }
    }
    private void LoadAudioClips()
    {
        for (int i = 0; i < syncTexts.Count; i++)
        {
            AudioClip Clip = audioSource[i].clip;
            audioClip.Add(Clip);
        }
    }
    public bool IsSyncFinish()
    {
        return syncTexts[syncTexts.Count-1].IsFinish;
    }
    public void HideAllSyncText()
    {
        foreach (var syncText in syncTexts)
        {
            syncText.gameObject.SetActive(false);
        }
    }
    public void ShowFirstSyncText()
    {
        syncTexts[0].gameObject.SetActive(true);
    }

    public List<SyncText> GetSyncTexts()
    {
        return syncTexts;
    }
}

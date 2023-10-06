using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageController : MonoBehaviour, IPageController
{
    [SerializeField] private List<SyncTextController> syncTexts;
    [SerializeField] private TouchManager touchManager;
    [SerializeField] protected List<AudioSource> audioSource;
    [SerializeField] private List<AudioClip> audioClip;
    public List<SyncTextController> SyncText { get => syncTexts; }
    public List<AudioClip> AudioClip { get => audioClip;}
    public TouchManager TouchManager { get => touchManager; set => touchManager = value; }

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
            if (i < syncTexts.Count - 1 && syncTexts[i].IsFinishSync())
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
        TouchManager = GetComponentInChildren<TouchManager>();
    }
    private void LoadSyncText()
    {
        SyncTextController[] texts = GetComponentsInChildren<SyncTextController>();
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
        return syncTexts[syncTexts.Count-1].IsFinishSync();
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

    public List<SyncTextController> GetSyncTexts()
    {
        return syncTexts;
    }
}

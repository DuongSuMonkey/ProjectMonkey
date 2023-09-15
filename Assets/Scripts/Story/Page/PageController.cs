using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] private List<SyncText> syncTexts;
    [SerializeField] protected TouchesController touchesController;
    [SerializeField] protected List<AudioSource> audioSource;
    [SerializeField] private List<AudioClip> audioClip;
    public List<SyncText> SyncText { get => syncTexts; }
    public List<AudioClip> AudioClip { get => audioClip;}

    private void Start()
    {
        HideAllChangeTextColors();
        ShowFirstChangeTextColor();
    }
    private void Reset()
    {
        LoadComponents();
    }
    private void Update()
    {
        for (int i = 0; i < syncTexts.Count; i++)
        {
            if (i < syncTexts.Count - 1 && syncTexts[i].IsFinal)
            {
                syncTexts[i].gameObject.SetActive(false);
                syncTexts[++i].gameObject.SetActive(true);
            }
        }
    }
    public void LoadComponents()
    {
        LoadTouches();
        LoadChangTextColors();
        LoadAudios();
        LoadAudioClips();
    }
    private void LoadTouches()
    {
        touchesController = GetComponentInChildren<TouchesController>();
    }
    private void LoadChangTextColors()
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
    public bool IsFinal()
    {
        return syncTexts[syncTexts.Count-1].IsFinal;
    }
    private void HideAllChangeTextColors()
    {
        foreach (var changeTextColor in SyncText)
        {
            changeTextColor.gameObject.SetActive(false);
        }
    }
    public void ShowFirstChangeTextColor()
    {
        syncTexts[0].gameObject.SetActive(true);
    }
   
}

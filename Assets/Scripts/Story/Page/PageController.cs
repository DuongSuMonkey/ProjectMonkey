using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] private List<SyncText> changeTextColor;
    [SerializeField] protected TouchesController touchesController;
    [SerializeField] protected List<AudioSource> audioSource;
    [SerializeField] private List<AudioClip> audioClip;
    public List<SyncText> ChangeTextColor { get => changeTextColor; }
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
        for (int i = 0; i < changeTextColor.Count; i++)
        {
            if (i < changeTextColor.Count - 1 && changeTextColor[i].IsFinal)
            {
                changeTextColor[i].gameObject.SetActive(false);
                changeTextColor[++i].gameObject.SetActive(true);
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
        changeTextColor.AddRange(texts);
    }
    private void LoadAudios()
    {
        for (int i = 0; i < changeTextColor.Count; i++)
        {
            AudioSource audio = ChangeTextColor[i].GetComponent<AudioSource>();
            audioSource.Add(audio);
        }
    }
    private void LoadAudioClips()
    {
        for (int i = 0; i < changeTextColor.Count; i++)
        {
            AudioClip Clip = audioSource[i].clip;
            audioClip.Add(Clip);
        }
    }
    public bool IsFinal()
    {
        return changeTextColor[changeTextColor.Count-1].IsFinal;
    }
    private void HideAllChangeTextColors()
    {
        foreach (var changeTextColor in ChangeTextColor)
        {
            changeTextColor.gameObject.SetActive(false);
        }
    }
    public void ShowFirstChangeTextColor()
    {
        changeTextColor[0].gameObject.SetActive(true);
    }
   
}

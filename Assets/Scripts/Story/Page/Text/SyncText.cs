using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncText :Texts
{
    [SerializeField] private PageController pageController;
    [SerializeField] private Color targetColor=Color.red;
    [SerializeField] private bool isFinal=false;
    [SerializeField] protected float timeChange;
    [SerializeField] protected float timer;
    public int syncDataIndex = 0;
    public bool IsFinal { get => isFinal;}
    public GetSyncDataFromJson getSyncDataFromJson;
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
       LoadPageController();
       LoadTimeText();
       GetTime();
       GetContent();
    }
    public void GetTime()
    {
        foreach (var data in getSyncDataFromJson.syncData) {
            syncData.Add(data);
        }
    }
    public void LoadTimeText()
    {
        getSyncDataFromJson = GetComponent<GetSyncDataFromJson>();
    }
    public void GetContent()
    {
        for(int i=0;i< getSyncDataFromJson.txtContents.Count;i++)
        {
            if (i > txtContents.Count - 1)
            {
                TextMeshProUGUI text = Instantiate(getSyncDataFromJson.textPrefab, this.transform);
                text.rectTransform.localPosition = Vector3.zero;
                text.rectTransform.localScale = Vector3.one;
                txtContents.Add(text);
            }
            txtContents[i].text = getSyncDataFromJson.txtContents[i];
        }
    }
    public override void LoadTexts()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        AddText(texts);
    }
    private void LoadPageController()
    {
        pageController = GetComponentInParent<PageController>();
    }
    private void Update()
    {
        ChangeTime();
        ChangeColor();

    }
    public void ChangeTextColorFinal()
    {
        txtContents[currentIndex - 1].color = Color.black;
        isFinal = true;
    }
    public void ChangeTime()
    {
        if (currentIndex < txtContents.Count)
        {
            timer += Time.deltaTime;
        }
        else
        {
            return;
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
        else if (currentIndex== txtContents.Count  && !isFinal)
        {
            Invoke(nameof(ChangeTextColorFinal), syncData[txtContents.Count - 1].timeEnd / 1000 - syncData[txtContents.Count - 1].timeStart / 1000);
        }
    }
}
[Serializable]
public struct SyncData {
    public float timeStart;
    public float timeEnd;
}


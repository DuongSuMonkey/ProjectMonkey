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
    public bool IsFinal { get => isFinal;}
    public bool isStartNow=true;
    void Start()
    {
        if (isStartNow)
        {
            timer = timeChange;
        }
    }
    private void Reset()
    {
        LoadComponents();
    }
    public void LoadComponents()
    {
       LoadTexts();
       LoadPageController();
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
        if (!IsFinal)
        {
            Invoke(nameof(ChangeTextColorFinal), this.pageController.AudioClip[0].length);
            return;
        }
    }
    public void ChangeTextColorFinal()
    {
        txtsContent[currentIndex - 1].color = Color.black;
        isFinal = true;
    }
    public void ChangeTime()
    {
        if (currentIndex < txtsContent.Count)
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
        
        if (timer >= timeChange && currentIndex < txtsContent.Count)
        {
            foreach(var txtContent in txtsContent)
            {
                txtContent.color = Color.black;
            }
            txtsContent[currentIndex].color = targetColor;
            currentIndex++;
            timer = 0.0f;
        }
    }

}


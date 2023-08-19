using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeTextColor : Texts
{
    public static ChangeTextColor Instance;
    [SerializeField] private Color color;
    [SerializeField] private bool isFinal=false;
    public bool IsFinal { get => isFinal; }
    void Start()
    {
        Instance= this;
        timer = timeChange;
    }
    private void Reset()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        txtsContent.AddRange(texts);
    }
    private void Update()
    {
        ChangeTime();
        ChangeColor();
        if(currentIndex== txtsContent.Count && IsFinal==false)
        {
            Invoke(nameof(ChangeTextColorFinal), 0.5f);
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
            txtsContent[currentIndex].color = color;
            currentIndex++;
            timer = 0.0f;
        }
    }
}


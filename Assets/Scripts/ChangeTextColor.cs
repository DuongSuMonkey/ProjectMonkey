using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeTextColor : MonoBehaviour
{
    public static ChangeTextColor Instance;
    [SerializeField] private List<TextMeshProUGUI> txtContent;
    [SerializeField] private Color color;
    [SerializeField] private float TimeChange;
    [SerializeField] private float timer;
    [SerializeField] private int textIndex = 0;
    [SerializeField] private bool isFinal=false;

    public bool IsFinal { get => isFinal; }

    void Start()
    {
        Instance= this;
        timer = TimeChange;
    }
    private void Reset()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        txtContent.AddRange(texts);
    }
    private void Update()
    {
       
       ChangeText();
        if(textIndex== txtContent.Count && IsFinal==false)
        {
            Invoke(nameof(ChangeTextColorFinal), 0.5f);
            return;
        }
    } 
    public void ChangeTextColorFinal()
    {
        txtContent[textIndex - 1].color = Color.black;
        isFinal = true;

    }
    public void ChangeText()
    {
        if (textIndex < txtContent.Count)
        {
            timer += Time.deltaTime;
        }
        else
        {
            return;
        }
        if (timer >= TimeChange && textIndex < txtContent.Count)
        {
            foreach(var txtContent in txtContent)
            {
                txtContent.color = Color.black;
            }
            txtContent[textIndex].color = color;
            textIndex++;
            timer = 0.0f;
        }
    }
}


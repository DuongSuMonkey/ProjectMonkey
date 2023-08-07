using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeTextColor : MonoBehaviour
{
   // [SerializeField]private List<string> content;
    [SerializeField] private List<TextMeshProUGUI> txtContent;
    [SerializeField] private Color color;
    [SerializeField] private float TimeChange;
    [SerializeField] private float timer;
    [SerializeField] private int i = 0;
    void Start()
    {
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
        if(i== txtContent.Count)
        {
            StartCoroutine(ChangeTextFinal());
        }
    } 
   IEnumerator ChangeTextFinal()
    {
        yield return new WaitForSeconds(TimeChange);
        txtContent[i-1].color = Color.black;
    }
    public void ChangeText()
    {
        if (i < txtContent.Count)
        {
            timer += Time.deltaTime;
        }
        else
        {
            return;
        }
        if (timer >= TimeChange && i < txtContent.Count)
        {
            for (int j = 0; j < txtContent.Count; j++)
            {
                txtContent[j].color = Color.black;
            }
            txtContent[i].color = color;
            i++;
            timer = 0.0f;
        }
    }
}


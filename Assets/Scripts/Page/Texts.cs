using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public abstract class  Texts : MonoBehaviour
{
    [SerializeField] protected List<TextMeshProUGUI> txtsContent;
    [SerializeField] protected int currentIndex = 0;
    public void AddText(TextMeshProUGUI[] texts)
    {
        txtsContent.AddRange(texts);
    }
    public abstract void LoadTexts();

}

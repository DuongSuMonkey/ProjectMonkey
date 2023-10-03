using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public abstract class  Texts : MonoBehaviour
{
    [SerializeField] public List<TextMeshProUGUI> txtContents;
    [SerializeField] protected int currentIndex = 0;
    public abstract void LoadTexts();

}

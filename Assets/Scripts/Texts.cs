using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public abstract class  Texts : MonoBehaviour
{
    [SerializeField] protected List<TextMeshProUGUI> txtsContent;
    [SerializeField] protected float timeChange;
    [SerializeField] protected float timer;
    [SerializeField] protected int currentIndex = 0;
}

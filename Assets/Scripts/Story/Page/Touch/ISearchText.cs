using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ISearchText 
{
    void Search(TouchUI touchUI, List<TextMeshProUGUI> txtsContent, MonoBehaviour obj);
}

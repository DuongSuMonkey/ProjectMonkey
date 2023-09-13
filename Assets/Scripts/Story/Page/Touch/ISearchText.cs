using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ISearchText 
{
    void Search(TouchUI touch, List<TextMeshProUGUI> txtsContent, MonoBehaviour obj);
    void OriginalTextColor(TextMeshProUGUI textcontent);
}

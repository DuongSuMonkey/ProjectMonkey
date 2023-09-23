using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public interface IPageController 
{
    bool IsFinal();
    void HideAllSyncText();
    void ShowFirstSyncText();
    void LoadTouches();
    void LoadTexts(List<TextMeshProUGUI> txtContents);
}

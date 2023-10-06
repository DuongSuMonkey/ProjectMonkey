using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public interface IPageController 
{
    List<SyncTextController> GetSyncTexts();
    bool IsSyncFinish();
    void ReLoad();
    void LoadTexts(List<TextMeshProUGUI> txtContents);
}

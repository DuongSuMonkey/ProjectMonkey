using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDownloadAssetbundle
{
    void GetAsset();
    void LoadSingleAsset();
    void LoadMultipleAssets();
    void LoadAllAssets();
}

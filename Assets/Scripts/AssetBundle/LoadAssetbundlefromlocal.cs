using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadAssetbundlefromlocal : IDownloadAssetbundle
{

    void Start()
    {
        GetAsset();
        LoadAsset();
    }

   public override void GetAsset()
   {
        myAsset = AssetBundle.LoadFromFile(path);
        Debug.Log(myAsset == null ? "Failed" : "Success");
   }

    public override void LoadAsset()
    {
        base.LoadAsset();
    }
    public override void LoadSingleAsset()
    {
        base.LoadSingleAsset();
    }

    public override void LoadMultipleAssets()
    {
        base.LoadMultipleAssets();
    }

    public override void LoadAllAssets()
    {
        base.LoadAllAssets();
    }
}


  public enum AssetLoadType
{
    SingleAsset,
    MultipleAssets,
    AllAssets
}


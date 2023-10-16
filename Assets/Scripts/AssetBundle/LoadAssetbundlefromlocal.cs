using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadAssetbundlefromlocal : DownloadAssetbundle
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
        LoadMaterial();
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
    private void LoadMaterial()
    {
        var assets = myAsset.LoadAllAssets();
        foreach (var prefab in assets)
        {
            if (prefab.GetType() == typeof(Material))
            {
                Material material = prefab as Material;
                Debug.Log(material);
           
            }
        }
    }
}


  public enum AssetLoadType
{
    SingleAsset,
    MultipleAssets,
    AllAssets
}


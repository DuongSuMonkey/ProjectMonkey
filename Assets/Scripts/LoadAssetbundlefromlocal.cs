using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetbundlefromlocal : MonoBehaviour
{
    // Start is called before the first frame update
    AssetBundle myAsset;
    public string path;
    public string assetName;
    void Start()
    {
        LoadAsset();
        InstantiateAssset();
    }

    void LoadAsset()
    {
        myAsset=AssetBundle.LoadFromFile(path);
        Debug.Log(myAsset == null ? "Faild" : "Succes");
        
    }
    public void InstantiateAssset()
    {
       var AllAsset= myAsset.LoadAllAssets();
        foreach(var Asset in AllAsset)
        {
            Debug.Log (Asset.name);
         //   Instantiate(Asset);
        }
        var prefab = myAsset.LoadAsset(assetName);
        Instantiate(prefab);
    }
}

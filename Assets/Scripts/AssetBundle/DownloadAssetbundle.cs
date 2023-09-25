using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDownloadAssetbundle:MonoBehaviour
{
    [SerializeField] protected AssetBundle myAsset;
    [SerializeField] protected string path;
    [SerializeField] protected AssetLoadType loadType;
    [SerializeField] protected string singleAssetName;
    [SerializeField] protected List<string> multipleAssetNames = new List<string>();
    [SerializeField] protected Canvas canvas;

    public  abstract void GetAsset();
  
    public virtual void LoadSingleAsset()
    {
        var prefab = myAsset.LoadAsset(singleAssetName);
        Instantiate(prefab, canvas.transform);
    }
    public virtual void LoadMultipleAssets()
    {
        foreach (var assetName in multipleAssetNames)
        {
            var prefab = myAsset.LoadAsset(assetName);
            if (prefab != null && prefab.GetType() == typeof(GameObject))
            {
                GameObject gameObject = prefab as GameObject;
                RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    Instantiate(gameObject, canvas.transform);
                }
                else
                {
                    Instantiate(gameObject);
                }
            }
        }
    }
    public virtual void LoadAllAssets()
    {
        var assets = myAsset.LoadAllAssets();
        foreach (var prefab in assets)
        {
            if (prefab.GetType() == typeof(GameObject))
            {
                GameObject gameObject = prefab as GameObject;
                RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    Instantiate(gameObject, canvas.transform);
                }
                else
                {
                    Instantiate(gameObject);
                }
            }
            else if (prefab.GetType() == typeof(AudioClip))
            {
                AudioClip audioClip = prefab as AudioClip;
                audioClip.LoadAudioData();
            }
        }
    }
    public virtual void LoadAsset()
    {
        switch (loadType)
        {
            case AssetLoadType.SingleAsset:
                if (!string.IsNullOrEmpty(singleAssetName))
                {
                    LoadSingleAsset();
                }
                break;

            case AssetLoadType.MultipleAssets:
                if (multipleAssetNames.Count > 0)
                {
                    LoadMultipleAssets();
                }
                break;

            case AssetLoadType.AllAssets:
                LoadAllAssets();
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadAssetbundlefromlocal : MonoBehaviour, IDownloadAssetbundle
{

    [SerializeField] private AssetBundle myAsset;
    public string path;
    [SerializeField] private AssetLoadType loadType;
    [SerializeField] private string singleAssetName;
    [SerializeField] private List<string> multipleAssetNames = new List<string>();
    [SerializeField] private Canvas canvas;

    void Start()
    {
        GetAsset();
        LoadAsset();
    }

   public void GetAsset()
    {
        myAsset = AssetBundle.LoadFromFile(path);
        Debug.Log(myAsset == null ? "Failed" : "Success");
    }

    public void LoadAsset()
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
    public void LoadSingleAsset()
    {
        var prefab = myAsset.LoadAsset(singleAssetName);
        Instantiate(prefab, canvas.transform);
    }

    public void LoadMultipleAssets()
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

    public void LoadAllAssets()
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
}


  public enum AssetLoadType
{
    SingleAsset,
    MultipleAssets,
    AllAssets
}


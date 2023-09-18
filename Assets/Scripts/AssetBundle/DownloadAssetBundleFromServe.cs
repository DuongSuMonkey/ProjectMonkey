using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundleFromServe : MonoBehaviour
{
    [SerializeField] private AssetBundle myAsset;
    [SerializeField] private string path;
    [SerializeField] private AssetLoadTypeFromSever loadType;
    [SerializeField] private Canvas canvas;
    [SerializeField] private string singleAssetName;
    [SerializeField] private List<string> multipleAssetNames = new List<string>();
    void Start()
    {
        GetAssets();
    }
    public void GetAssets()
    {
        StartCoroutine(Download());
    }
    private IEnumerator Download()
    {
        string url = path;
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {

            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error" + url + "" + www.error);
            }
            else
            {
                Debug.Log(www);
                myAsset = DownloadHandlerAssetBundle.GetContent(www);
                LoadAudioData();
                switch (loadType)
                {
                    case AssetLoadTypeFromSever.SingleAsset:
                        if (!string.IsNullOrEmpty(singleAssetName))
                        {
                            LoadSingleAsset();
                        }
                        break;

                    case AssetLoadTypeFromSever.MultipleAssets:
                        if (multipleAssetNames.Count > 0)
                        {
                            LoadMultipleAssets();
                        }
                        break;

                    case AssetLoadTypeFromSever.AllAssets:
                        LoadAllAssets();
                        break;
                }
                myAsset.Unload(false);
            }
                www.Dispose();
            }
        }
    private void LoadAudioData()
    {
        var assets = myAsset.LoadAllAssets();
        foreach (var prefab in assets)
        {
            if (prefab.GetType() == typeof(AudioClip))
            {
                AudioClip audioClip = prefab as AudioClip;
                audioClip.LoadAudioData();
            }
        }
    }
    private void LoadSingleAsset()
    {
        var prefab = myAsset.LoadAsset(singleAssetName);
        Instantiate(prefab, canvas.transform);
    }

    private void LoadMultipleAssets()
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

    private void LoadAllAssets()
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
        }
    }
}
public enum AssetLoadTypeFromSever
{
    SingleAsset,
    MultipleAssets,
    AllAssets
}



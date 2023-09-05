using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundleFromServe : MonoBehaviour
{
    [SerializeField] private AssetBundle bundle;
    [SerializeField] private string path;
    [SerializeField] private string assetName;
    [SerializeField] private Canvas canvas;
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
                bundle = DownloadHandlerAssetBundle.GetContent(www);
                if (assetName != "")
                {
                    LoadAsset();
                }
                else
                {
                    LoadAllAssets();
                }
                    bundle.Unload(false);
            }
                www.Dispose();
            }
        }
    private void LoadAsset()
    {
        var asset = bundle.LoadAsset(assetName);
        if (asset.GetType() == typeof(GameObject))
        {
            GameObject gameObject = asset as GameObject;
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
    private void LoadAllAssets()
    {
        var assets = bundle.LoadAllAssets();
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

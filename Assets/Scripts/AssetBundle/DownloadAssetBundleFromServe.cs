using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundleFromServe :IDownloadAssetbundle
{
    void Start()
    {
        GetAsset();
    }
    public override void GetAsset()
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
                base.LoadAsset();
                myAsset.Unload(false);
            }
                www.Dispose();
            }
        }
    private  void LoadAudioData()
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



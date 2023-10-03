using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadAssetBundleFromServe :DownloadAssetbundle
{
    public Slider progressBar;
    public TextMeshProUGUI txtProgress;
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
            www.SendWebRequest();
            while (!www.isDone)
            {
                float progress = Mathf.Clamp01(www.downloadProgress / 0.9f);
                txtProgress.text = progress.ToString("P0");
                progressBar.value = progress;
                yield return null;
            }
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



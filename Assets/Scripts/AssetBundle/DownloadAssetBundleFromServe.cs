using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadAssetBundleFromServe :DownloadAssetbundle
{
    public Slider progressBar;
    public TextMeshProUGUI txtProgress;
   [SerializeField] private string localPath;
    void Start()
    {
        localPath= Application.persistentDataPath + "/AssetBundles/" + "Assetbundle" ;
      // localPath = Path.Combine(Application.persistentDataPath, "AssetBundle\\story1");
        if (File.Exists(localPath))
        {
            progressBar.gameObject.SetActive(false);
            LoadLocalAssetBundle();
        }
        else
        {
            GetAsset();
        }
    }
    public override void GetAsset()
    {
        StartCoroutine(Download());
    }
    private void LoadLocalAssetBundle()
    {
        byte[] bundleData = File.ReadAllBytes(localPath);
        myAsset = AssetBundle.LoadFromMemory(bundleData);
        LoadAudioData();
        base.LoadAsset();
        myAsset.Unload(false);
    }

    [Obsolete]
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
                WWW objSERVER = new WWW(url);
                yield return objSERVER;
                SaveDownloadedAsset(objSERVER);
                LoadAudioData();
                base.LoadAsset();
                myAsset.Unload(false);
                Debug.Log("succes");
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

    [System.Obsolete]
    public void SaveDownloadedAsset(WWW objSERVER)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/AssetBundles"))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/AssetBundles");
		}
        byte[] bytes = objSERVER.bytes;
        File.WriteAllBytes(localPath, bytes);
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



using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundleFromServe : MonoBehaviour
{
    AssetBundle bundle;
    public string path;
    public string assetName;
    public Camera Cam;
    public Canvas canvas;
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
        //var asset;
        // string url = "https://drive.google.com/u/0/uc?id=1FGB7ut-vn9BQzF7l5Xerf9buYQDojy0j&export=download";
        //string url = "https://drive.google.com/u/0/uc?id=1tOJxWiWphaYqGLcwkSRc6H2JKm2XTGnU&export=download";
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
                    var assets = bundle.LoadAllAssets();
                    // var assets = bundle.LoadAllAssets();
                    var asset = bundle.LoadAsset(assetName);
                    if (assetName == "Canvas")
                    {
                        canvas.gameObject.SetActive(false);
                        asset.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                        asset.GetComponent<Canvas>().worldCamera = Cam;
                        Instantiate(asset);
                    }
                    else
                    {
                        canvas.gameObject.SetActive(true);
                        Instantiate(asset, canvas.transform);
                        foreach (var asset1 in assets)
                        {
                            if (asset1.GetType() == typeof(GameObject))
                            {
                                Debug.Log(asset1.name);
                            }
                            else if (asset1.GetType() == typeof(AudioClip))
                            {
                                AudioClip audioClip = asset1 as AudioClip;
                                audioClip.LoadAudioData();
                            }
                        }
                        foreach (var prefab in assets)
                        {
                            if (prefab.name == "MenuPanel")
                            {
                                Instantiate(prefab, canvas.transform);
                            }
                        }
                        
                    }
                } 
                bundle.Unload(false);
            }
            www.Dispose();
        }
   
    }

}

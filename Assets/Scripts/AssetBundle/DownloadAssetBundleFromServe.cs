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
                var assets = bundle.LoadAllAssets();
                foreach (var a in assets)
                {
                    Debug.Log(a.name);
                    //if (a.name == "Main Camera")
                    //{
                    //     Instantiate(a);
                    //}
                }
                if (assetName != "")
                {
                    var asset = bundle.LoadAsset(assetName);
                    Instantiate(asset);
                    //Camera Cam=  asset.GetComponentInChildren<Camera>();
                    //asset.GetComponentInChildren<Canvas>().worldCamera = Cam;
                }
                
                bundle.Unload(false);
            }
            www.Dispose();
        }
   
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundleFromServe : MonoBehaviour
{
     void Start()
     {
        StartCoroutine(Download());

     }
    private IEnumerator Download()
    {
        //var asset;
       // string url = "https://drive.google.com/u/0/uc?id=1FGB7ut-vn9BQzF7l5Xerf9buYQDojy0j&export=download";
        string url = "https://drive.google.com/u/0/uc?id=1tOJxWiWphaYqGLcwkSRc6H2JKm2XTGnU&export=download";
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
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                var  assets = bundle.LoadAllAssets();
                foreach(var a in assets)
                {
                    Debug.Log(a.name);
                }
                bundle.Unload(false);
            }
            www.Dispose();
        }
     // InstantiateGameobjectFromAsset(asset);

    }
    private void InstantiateGameobjectFromAsset(GameObject go)
    {
        GameObject instantiate = Instantiate(go);
    }
}

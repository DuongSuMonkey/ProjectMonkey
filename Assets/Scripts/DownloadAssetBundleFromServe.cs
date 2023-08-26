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
        GameObject asset = null;
       // string url = "https://drive.google.com/u/0/uc?id=1FGB7ut-vn9BQzF7l5Xerf9buYQDojy0j&export=download";
        string url = "https://drive.google.com/u/0/uc?id=1EsCN7l8G_ZVbvUuGceqab6KXskvJYy7w&export=download";
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
                asset = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle = AssetBundle.LoadFromFile("D:\\ProjectMonkey\\AssetBundle");
                bundle.Unload(false);
                //  AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                //  // Load the asset from the AssetBundle.
                //  asset = (GameObject)bundle.LoadAsset("./images");
                // // Do something with the asset.
                ////  Debug.Log("Asset loaded: " + asset.name);
                //  bundle.Unload(false);
            }
            www.Dispose();
        }
      InstantiateGameobjectFromAsset(asset);

    }
    private void InstantiateGameobjectFromAsset(GameObject go)
    {
        GameObject instantiate = Instantiate(go);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadAssetbundlefromlocal : MonoBehaviour
{
    // Start is called before the first frame update
    AssetBundle myAsset;
    public string path;
    public string assetName;
    public Camera Cam;
    void Start()
    {
        LoadAsset();
        InstantiateAsset();
    }

    void LoadAsset()
    {
        myAsset=AssetBundle.LoadFromFile(path);
        Debug.Log(myAsset == null ? "Faild" : "Succes");
        
    }
    public void InstantiateAsset()
    {
        var allAsset = myAsset.LoadAllAssets();
        foreach (var Asset in allAsset)
        {
            Debug.Log(Asset.name);
        }
        if (assetName != "")
        {
            var prefab = myAsset.LoadAsset(assetName);
            if (assetName == "Canvas")
            {
                prefab.GetComponentInChildren<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                prefab.GetComponentInChildren<Canvas>().worldCamera = Cam;
                Debug.Log(prefab.GetComponentInChildren<Canvas>().worldCamera);
            }

            Instantiate(prefab);
           
        }
    }
    //void InstantiateAsset()
    //{
    //    // Load all assets within the assetbundle
    //    var allAssets = myAsset.LoadAllAssets();

    //    foreach (var asset in allAssets)
    //    {
    //        if (asset.GetType() == typeof(GameObject))
    //        {
    //            Debug.Log(asset.name);
    //            //Instantiate(asset as GameObject);
    //        }
    //        else if (asset.GetType() == typeof(AudioClip))
    //        {
    //            // Here you can handle audio assets as needed
    //            // For example, play the audio clip
    //            AudioClip audioClip = asset as AudioClip;
    //            AudioSource.PlayClipAtPoint(audioClip, transform.position);
    //        }
    //        else if (asset.GetType() == typeof(Texture2D))
    //        {
    //            // Here you can handle texture assets as needed
    //            // For example, assign texture to a material
    //            Texture2D texture = asset as Texture2D;
    //            Material material = GetComponent<Renderer>().material;
    //            material.mainTexture = texture;
    //        }
    //    }

    //    // Load a specific asset by name, if provided
    //    if (!string.IsNullOrEmpty(assetName))
    //    {
    //        var prefab = myAsset.LoadAsset(assetName);
    //        if (prefab != null && prefab.GetType() == typeof(GameObject))
    //        {
    //            Instantiate(prefab as GameObject);
    //        }
    //    }
    // }
}

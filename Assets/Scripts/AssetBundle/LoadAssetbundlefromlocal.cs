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
    public Canvas canvas;
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
        if (assetName != "")
        {
            var allAsset = myAsset.LoadAllAssets();
            var prefab = myAsset.LoadAsset(assetName);
            if (assetName == "Canvas")
            {
                canvas.gameObject.SetActive(false);
                prefab.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                prefab.GetComponent<Canvas>().worldCamera = Cam;
                Debug.Log(prefab.GetComponent<Canvas>().worldCamera);
                Instantiate(prefab);
            }
            else
            {
                canvas.gameObject.SetActive(true);
                Instantiate(prefab, canvas.transform);
                foreach (var Asset in allAsset)
                {
                    if (Asset.name == "MenuPanel")
                    {
                        Instantiate(Asset, canvas.transform);
                    }
                }
            }
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

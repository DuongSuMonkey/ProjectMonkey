using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadAssetbundlefromlocal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AssetBundle myAsset;
    public string path;
    [SerializeField] private string assetName;
    [SerializeField] private Canvas canvas;
    void Start()
    {
        GetAsset();
        InstantiateAsset();
    }

    void GetAsset()
    {
        myAsset = AssetBundle.LoadFromFile(path);
        Debug.Log(myAsset == null ? "Faild" : "Succes");

    }
    public void InstantiateAsset()
    {
        if (assetName != "")
        {
            LoadAsset();
        }
        else
        {
            LoadAllAssets();
        }
        #region
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
        #endregion
    }
    private void LoadAsset()
    {
        var prefab = myAsset.LoadAsset(assetName);
        Instantiate(prefab, canvas.transform);
    }
    private void LoadAllAssets()
    {
        var assets = myAsset.LoadAllAssets();
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


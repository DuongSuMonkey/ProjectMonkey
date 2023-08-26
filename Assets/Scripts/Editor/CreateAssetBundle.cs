using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System;

public class CreateAssetBundle 
{

    [MenuItem("Assets/Create Assets Bundle")]
    private static void BuildAllAsset()
    {

        string path = Application.dataPath + "/../AssetBundle";
        try
        {
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}

using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SyncDataRetriever : MonoBehaviour, ISyncDataRetriever
{
    [Header("Path")]
    [SerializeField] public string syncDataPath = "";
    [SerializeField] private List<string> filesPath;
    [SerializeField] private string pathPage;
    [Header("Components")]
    [SerializeField] private JsonSyncDataPath jsonPath;
    [SerializeField] private IPageController pageController;
    [SerializeField] private IGetSyncData getSyncData;
    [SerializeField] private SyncTextController syncTextController;
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private List<SyncData> syncData;
    private void Reset()
    {
        LoadComponents();
    }
    public void LoadComponents()
    {
        GetPagePath();
        getSyncData = new GetSyncData(syncDataPath, syncData);
        GetSyncDataPath();
        GetPageController();
        SetSyncDataPath();
        GetTextPrefab();
        GetSyncDatas();
        GetSyncText();
        SetDataSyncText();
    }
    public void GetPagePath()
    {
        jsonPath = GetComponent<JsonSyncDataPath>();
        pathPage = jsonPath.pathPage;
    }
    public void GetPageController()
    {
        pageController = GetComponentInParent<IPageController>();
    }
    public void GetSyncText()
    {
        syncTextController = GetComponent<SyncTextController>();
    }
    public void GetTextPrefab()
    {
        textPrefab = jsonPath.textPrefab;
    }
    public void GetSyncDataPath()
    {
        filesPath=getSyncData.GetSyncDataPaths(pathPage, syncDataPath, filesPath);
    }
    public void GetSyncDatas()
    {
        getSyncData.GetSyncDatas(syncDataPath);
    }
    public void SetSyncDataPath()
    {
        for (int i = 0; i < pageController.GetSyncTexts().Count; i++)
        {
            pageController.GetSyncTexts()[i].GetComponent<SyncDataRetriever>().syncDataPath = filesPath[i];
        }
    }
    public void SetDataSyncText()
    {
        for (int i = 0; i < syncData.Count; i++)
        {
            if (i > syncTextController.txtContents.Count - 1)
            {
                TextMeshProUGUI text = Instantiate(textPrefab, this.transform);
                text.rectTransform.localPosition = Vector3.zero;
                text.rectTransform.localScale = Vector3.one;
                syncTextController.txtContents.Add(text);
            }
            syncTextController.txtContents[i].text = syncData[i].word;
        }
        for (int i = 0; i < syncData.Count; i++)
        {
            if (i > syncTextController.syncData.Count - 1)
            {
                syncTextController.syncData.Add(syncData[i]);
            }
            syncTextController.syncData[i] = syncData[i];
        }

    }
}

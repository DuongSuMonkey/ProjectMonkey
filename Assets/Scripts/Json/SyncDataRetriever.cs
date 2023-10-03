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
    [SerializeField] private SyncText syncText;
    [SerializeField] private List<string> txtContents;
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private List<SyncData> syncData;
    private void Reset()
    {
        LoadComponents();
    }
    public void LoadComponents()
    {
        GetPagePath();
        getSyncData = new GetSyncData(syncDataPath, txtContents, syncData);
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
        syncText = GetComponent<SyncText>();
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
        for (int i = 0; i < pageController.getSyncTexts().Count; i++)
        {
            pageController.getSyncTexts()[i].GetComponent<SyncDataRetriever>().syncDataPath = filesPath[i];
        }
    }
    public void SetDataSyncText()
    {
        for (int i = 0; i < txtContents.Count; i++)
        {
            if (i > syncText.txtContents.Count - 1)
            {
                TextMeshProUGUI text = Instantiate(textPrefab, this.transform);
                text.rectTransform.localPosition = Vector3.zero;
                text.rectTransform.localScale = Vector3.one;
                syncText.txtContents.Add(text);
            }
            syncText.txtContents[i].text = txtContents[i];
        }
        foreach (var data in syncData)
        {
            syncText.syncData.Add(data);
        }
      
    }
}

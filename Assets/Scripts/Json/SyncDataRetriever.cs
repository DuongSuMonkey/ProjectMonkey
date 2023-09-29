using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SyncDataRetriever : MonoBehaviour, ISyncDataRetriever
{
    [Header("Path")]
    [SerializeField] public string syncDataPath = "";
    public List<string> filesPath;
    public string pathPage;
    [Header("Components")]
    [SerializeField] private JsonSyncDataPath jsonPath;
    public PageController pageController;
    [SerializeField] private SyncText syncText;
    public List<string> txtContents;
    public TextMeshProUGUI textPrefab;
    public List<SyncData> syncData;
    private IGetSyncData getSyncData;
    private void Reset()
    {
        GetSyncDataPath();
        GetSyncText();
        GetPageController();
        SetSyncDataPath();
        GetTextPrefab();
        getSyncData = new GetSyncData(syncDataPath, txtContents, syncData);
        GetSyncDatas();
    }
    public void GetSyncDataPath()
    {
        jsonPath = GetComponent<JsonSyncDataPath>();
        pathPage = jsonPath.pathPage;
        string jsonContent = File.ReadAllText(pathPage);
        JObject jsonObject = JObject.Parse(jsonContent);
        JArray touchArray = (JArray)jsonObject["text"];
        for (int i = 0; i < touchArray.Count; i++)
        {
            int items = (int)jsonObject["text"][i]["word_id"];
            string folderPath = @"d:\4057_1_4307_1688701526\4057_1_4307_1688701526\word\";
            string[] files = Directory.GetFiles(folderPath, items + ".json", SearchOption.AllDirectories);
            syncDataPath = files[0];
            filesPath.AddRange(files);
        }
       
    }
    public void GetPageController()
    {
        pageController = GetComponentInParent<PageController>();
    }
    public void SetSyncDataPath()
    {
        for (int i = 0; i < pageController.SyncText.Count; i++)
        {
            pageController.SyncText[i].GetComponent<SyncDataRetriever>().syncDataPath = filesPath[i];
        }
    }
    public void GetTextPrefab()
    {
        textPrefab=jsonPath.textPrefab;
    }
    public void GetSyncText()
    {
        syncText = GetComponent<SyncText>();
    }
    public void GetSyncDatas()
    {
        getSyncData.GetSyncDatas(syncDataPath);
    }
}

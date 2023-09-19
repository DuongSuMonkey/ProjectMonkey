using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GetSyncDataFromJson : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private string path = "";
    public List<string> filesPath;
    public string pathPage;
    [Header("Components")]
    [SerializeField] private JsonSyncDataPath jsonPath;
    public PageController pageController;
    [SerializeField] private SyncText syncText;
    public List<string> txtContents;
    public TextMeshProUGUI textPrefab;
    public List<SyncData> syncData;
    private void Reset()
    {
        GetPath();
        GetSyncText();
        GetPageController();
        SetSyncDataPath();
        GetTextPrefab();
        GetSyncData();
    }
    public void GetPath()
    {
        jsonPath = GetComponent<JsonSyncDataPath>();
        pathPage = jsonPath.path;
        string jsonContent = File.ReadAllText(pathPage);
        JObject jsonObject = JObject.Parse(jsonContent);
        JArray touchArray = (JArray)jsonObject["text"];
        for (int i = 0; i < touchArray.Count; i++)
        {
            int items = (int)jsonObject["text"][i]["word_id"];
            string folderPath = @"d:\4057_1_4307_1688701526\4057_1_4307_1688701526\word\";
            string[] files = Directory.GetFiles(folderPath, items + ".json", SearchOption.AllDirectories);
            path = files[0];
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
            pageController.SyncText[i].GetComponent<GetSyncDataFromJson>().path = filesPath[i];
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
    public void GetSyncData()
    {
        string jsonContent = File.ReadAllText(path);
        JObject jsonObject = JObject.Parse(jsonContent);
        JArray touchArray = (JArray)jsonObject["audio"];
        for (int i = 0; i < touchArray.Count; i++)
        {
            JArray datas = (JArray)jsonObject["audio"][0]["sync_data"];
            for (int j = 0; j < datas.Count; j++)
            {
                float e =float.Parse( datas[j]["e"].ToString());
                float s = float.Parse(datas[j]["s"].ToString());
                string text = datas[j]["w"].ToString();
                txtContents.Add(text);
                SyncData newSyncData = new SyncData();
                newSyncData.timeStart = s;
                newSyncData.timeEnd = e;
                syncData.Add(newSyncData);
               
            }
        }
    }
}

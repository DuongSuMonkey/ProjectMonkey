using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GetSyncDataFromJson : MonoBehaviour
{
    [SerializeField] private JsonSyncDataPath jsonPath;
    [SerializeField] private string path = "";
    [SerializeField] private SyncText syncText;
    public List<string> txtContents;
    public TextMeshProUGUI textPrefab;
    public List<SyncData> syncData;
    private void Reset()
    {
        GetPath();
        GetSyncText();
        GetTextPrefab();
        GetSyncData();
    }
    public void GetPath()
    {
        jsonPath = GetComponent<JsonSyncDataPath>();
        path = jsonPath.path;
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
            JArray items = (JArray)jsonObject["audio"][0]["sync_data"];
            for (int j = 0; j < items.Count; j++)
            {
                float e =float.Parse( items[j]["e"].ToString());
                float s = float.Parse(items[j]["s"].ToString());
                string text = items[j]["w"].ToString();
                txtContents.Add(text);
                SyncData newSyncData = new SyncData();
                newSyncData.timeStart = s;
                newSyncData.timeEnd = e;
                syncData.Add(newSyncData);
               
            }
        }
    }
}

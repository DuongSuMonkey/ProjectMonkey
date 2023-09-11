using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GetTextTimeFromJson : MonoBehaviour
{
    [SerializeField] private JsonTimePath jsonPath;
    [SerializeField] private string path = "";
    [SerializeField] private SyncText syncText;
    public List<float> start;
    public List<float> end;
    private void Reset()
    {
        GetPath();
        GetSyncText();
        GetTime();
    }
    public void GetPath()
    {
        jsonPath = GetComponent<JsonTimePath>();
        path = jsonPath.path;
    }
    public void GetSyncText()
    {
        syncText = GetComponent<SyncText>();
    }
    public void GetTime()
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
                start.Add(s);
                end.Add(e);
            }
        }
    }
}

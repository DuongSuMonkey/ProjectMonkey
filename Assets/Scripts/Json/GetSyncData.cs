using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class GetSyncData : IGetSyncData
{
    private string path;
    private List<SyncData> syncData;
    public GetSyncData(string path, List<SyncData> syncData)
    {
        this.path = path;
        this.syncData = syncData;
    }

    public void GetSyncDatas(string path)
    {
        string jsonContent = File.ReadAllText(path);
        JObject jsonObject = JObject.Parse(jsonContent);
        JArray touchArray = (JArray)jsonObject["audio"];
        for (int i = 0; i < touchArray.Count; i++)
        {
            JArray datas = (JArray)jsonObject["audio"][0]["sync_data"];
            for (int j = 0; j < datas.Count; j++)
            {
                float e = float.Parse(datas[j]["e"].ToString());
                float s = float.Parse(datas[j]["s"].ToString());
                string text = datas[j]["w"].ToString();
                SyncData newSyncData = new SyncData();
                newSyncData.timeStart = s;
                newSyncData.timeEnd = e;
                newSyncData.word=text;
                syncData.Add(newSyncData);

            }
        }
    }
    public List<string> GetSyncDataPaths(string pathPage, string syncDataPath, List<string> filesPath)
    {
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
        return filesPath;
    }
}

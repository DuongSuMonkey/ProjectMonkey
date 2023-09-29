using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GetSyncData : IGetSyncData
{
    private string path;
    private List<string> txtContents;
    private List<SyncData> syncData;
    public GetSyncData(string path, List<string> txtContents, List<SyncData> syncData)
    {
        this.path = path;
        this.txtContents = txtContents;
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
                txtContents.Add(text);
                SyncData newSyncData = new SyncData();
                newSyncData.timeStart = s;
                newSyncData.timeEnd = e;
                syncData.Add(newSyncData);

            }
        }
    }
}

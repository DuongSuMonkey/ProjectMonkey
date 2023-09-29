using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetWordByID : ISetWordByID
{
    [SerializeField] private string path;
    [SerializeField] private List<PolygonCollider2D> polygonCollider2D;
    public SetWordByID (string path, List<PolygonCollider2D> polygonCollider2D)
    {
        this.path = path;
        this.polygonCollider2D = polygonCollider2D;
    }
    public void SetWordById(string path)
    {
        string jsonContent = File.ReadAllText(path);
        JObject jsonObject = JObject.Parse(jsonContent);
        for (int i = 0; i < polygonCollider2D.Count; i++)
        {
            int wordID = (int)jsonObject["image"][i]["word_id"];
            GetWordByID(wordID.ToString(),polygonCollider2D[i]);
        }
    }
    public void GetWordByID(string fileName, PolygonCollider2D polygonCollider)
    {
        string folderPath = @"d:\4057_1_4307_1688701526\4057_1_4307_1688701526\word\";
        string[] files = Directory.GetFiles(folderPath, fileName + ".json", SearchOption.AllDirectories);
        if (files.Length > 0)
        {
            string jsonFilePath = files[0];
            string jsonContent = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JObject.Parse(jsonContent);
            string data = (string)jsonObject["text"];
            polygonCollider.gameObject.GetComponentInChildren<TouchUI>().txtContent.text = data;
            string audioClipPath = $"audios/{data}";
            polygonCollider.gameObject.GetComponentInChildren<TouchUI>().audioSource.clip = Resources.Load<AudioClip>(audioClipPath);
            if (Resources.Load<AudioClip>(audioClipPath) == null)
            {
                Debug.Log("Audi not found");
            }
            polygonCollider.gameObject.GetComponentInChildren<TouchUI>().Reset();
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}

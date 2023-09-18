using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class JsonColliderBuilder : MonoBehaviour
{
    [SerializeField] private JsonPagePath jsonPath;
    [SerializeField] private string path = "";
    [SerializeField] private List<PolygonCollider2D> polygonCollider2D;
    [SerializeField] private List<Vector2> vertices = new List<Vector2>();
    [SerializeField] private TouchObject touchObject;
    private void Reset()
    {
        GetPath();
        GetTouchObjectPrefab();
        GetCollider();
        DrawCollider();
        SetWordById();
    }
    public void GetPath()
    {
        jsonPath = GetComponent<JsonPagePath>();
        path = jsonPath.path;
    }
    public void GetTouchObjectPrefab()
    {
        touchObject = jsonPath.touchObjectPrefab;
    }
    public void GetCollider()
    {
        PolygonCollider2D[] polygon = GetComponentsInChildren<PolygonCollider2D>();
        polygonCollider2D.AddRange(polygon);
    }

    public void DrawCollider()
    {
        string jsonContent = File.ReadAllText(path);
        JObject jsonObject = JObject.Parse(jsonContent);
        JArray touchArray = (JArray)jsonObject["image"];
        for (int i = 0; i < touchArray.Count; i++)
        {
            JArray items = (JArray)jsonObject["image"][i]["touch"][0]["vertices"];
            for (int j = 0; j < items.Count; j++)
            {
                var result = items[j].ToObject<string>();
                result = result.Trim('{', '}');
                string[] values = result.Split(',');
                int x = int.Parse(values[0]);
                int y = int.Parse(values[1]);
                Vector2 vector = new Vector2(x, y);
                if (!string.IsNullOrEmpty(result))
                {
                    vertices.Add(vector);
                }
            }
            if (vertices.Count > 0)
            {
                if (i >polygonCollider2D.Count - 1)
                {
                    TouchObject touch = Instantiate(touchObject,this.transform);
                    polygonCollider2D.Add(touch.GetComponent<PolygonCollider2D>());

                }
                polygonCollider2D[i].SetPath(0, vertices.ToArray());
                vertices.Clear();
            }
        }
    }
    public void SetWordById()
    {
        string jsonContent = File.ReadAllText(path);
        JObject jsonObject = JObject.Parse(jsonContent);
        for (int i = 0; i < polygonCollider2D.Count; i++)
        {
            int items = (int)jsonObject["image"][i]["word_id"];
            GetWordByID(items.ToString(), polygonCollider2D[i]);
        }
    }
    public void GetWordByID(string fileName,PolygonCollider2D polygonCollider)
    {
        string folderPath = @"d:\4057_1_4307_1688701526\4057_1_4307_1688701526\word\";
        string[] files = Directory.GetFiles(folderPath, fileName + ".json", SearchOption.AllDirectories);
        if (files.Length > 0)
        {
            string jsonFilePath = files[0];
            string jsonContent = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JObject.Parse(jsonContent);
            string items = (string)jsonObject["text"];
            polygonCollider.gameObject.GetComponentInChildren<TouchUI>().txtContent.text = items;
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}


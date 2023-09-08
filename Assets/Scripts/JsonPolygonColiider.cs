using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using System.Linq;
using System.IO;

public class JsonPolygonCollider : MonoBehaviour
{
    //public string path = "D:\\4057_1_4307_1688701526\\4057_1_4307_1688701526\\4131_page_1.json";
    //public PolygonCollider2D polygonCollider2D;
    //public List<Vector2> vertices = new List<Vector2>();

    //private void Reset()
    //{
    //    polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();
    //    string jsonContent = File.ReadAllText(path);
    //    JObject jsonObject = JObject.Parse(jsonContent);
    //    JArray touchArray = (JArray)jsonObject["image"];

    //    for (int i = 0; i < touchArray.Count; i++)
    //    {
    //        JArray items = (JArray)jsonObject["image"][i]["touch"][0]["vertices"];
    //        // Debug.Log(items);
    //        if (i == 0)
    //        {
    //            for (int j = 0; j < items.Count; j++)
    //            {
    //                // Debug.Log(items[j]);
    //                var result = items[j].ToObject<string>();
    //                //  Debug.Log(result);
    //                result = result.Trim('{', '}');
    //                string[] values = result.Split(',');
    //                int x = int.Parse(values[0]);
    //                int y = int.Parse(values[1]);
    //                Vector2 vector = new Vector2(x, y);
    //                vertices.Add(vector);
    //            }
    //            polygonCollider2D.SetPath(i, vertices.ToArray());
    //        }

    //    }
    //}
    public string path = "D:\\4057_1_4307_1688701526\\4057_1_4307_1688701526\\4131_page_1.json";
    public List<PolygonCollider2D> polygonCollider2D;
    public List<Vector2> vertices = new List<Vector2>();

    private void Reset()
    {
        PolygonCollider2D[] polygon = GetComponentsInChildren<PolygonCollider2D>();
        polygonCollider2D.AddRange(polygon);
        string jsonContent = File.ReadAllText(path);
        JObject jsonObject = JObject.Parse(jsonContent);
        JArray touchArray = (JArray)jsonObject["image"];

        for (int i = 0; i < polygonCollider2D.Count; i++)
        {
            JArray items = (JArray)jsonObject["image"][i]["touch"][0]["vertices"];
            if (i == 0)
            {
                for (int j = 0; j < items.Count; j++)
                {
                    var result = items[j].ToObject<string>();
                    result = result.Trim('{', '}');
                    string[] values = result.Split(',');
                    int x = int.Parse(values[0]);
                    int y = int.Parse(values[1]);
                    Vector2 vector = new Vector2(x, y);
                    vertices.Add(vector);
                }
                polygonCollider2D[i].SetPath(0, vertices.ToArray());
                vertices.Clear();
            }

        }
    }
}



using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DrawCollider : IDrawCollider
{
    [SerializeField] private string path;
    [SerializeField] private List<PolygonCollider2D> polygonCollider2D;
    [SerializeField] private List<Vector2> vertices;
    [SerializeField] private TouchObject touchObject;
    public DrawCollider(string path, List<PolygonCollider2D> polygonCollider2D, List<Vector2> vertices, TouchObject touchObject)
    {
        this.path = path;
        this.polygonCollider2D = polygonCollider2D;
        this.vertices= vertices;
        this.touchObject = touchObject;
    }
    public void DrawColliders(string path,MonoBehaviour obj)
    {
        string jsonContent = File.ReadAllText(path);
        JObject jsonObject = JObject.Parse(jsonContent);
        JArray touchArray = (JArray)jsonObject["image"];
        for (int i = 0; i < touchArray.Count; i++)
        {
            JArray datas = (JArray)jsonObject["image"][i]["touch"][0]["vertices"];
            for (int j = 0; j < datas.Count; j++)
            {
                var result = datas[j].ToObject<string>();
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
                if (i > polygonCollider2D.Count - 1)
                {
                    TouchObject touch = Object.Instantiate(touchObject, obj.transform);
                    polygonCollider2D.Add(touch.GetComponent<PolygonCollider2D>());
                }
                polygonCollider2D[i].SetPath(0,vertices.ToArray());
                vertices.Clear();
            }
        }
    }
}

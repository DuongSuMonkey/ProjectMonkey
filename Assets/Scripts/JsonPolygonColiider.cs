using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class JsonPolygonCollider : MonoBehaviour
{
    public string path;
    private void Start()
    {
        // Đọc nội dung của tệp JSON
        string jsonContent = System.IO.File.ReadAllText(path);
        //// Lặp qua các đối tượng JSON
        JObject jsonObject = JObject.Parse(jsonContent);
        JArray imageArray = (JArray)jsonObject["image"];
        for (int i = 0; i < imageArray.Count; i++)
        {
            JArray items = (JArray)jsonObject["image"][i]["touch"][0]["vertices"];
            Debug.Log(items);
        }
        //Vector2[] vertices = new Vector2[items.Count];
        //for (int i = 0; i < items.Count; i++)
        //{
        //    string vertexString = items[i].ToString();
        //    vertexString = vertexString.Replace("{", "").Replace("}", ""); // Loại bỏ các dấu ngoặc
        //    string[] coordinates = vertexString.Split(',');
        //    float x = float.Parse(coordinates[0]);
        //    float y = float.Parse(coordinates[1]);
        //    vertices[i] = new Vector2(x, y);
        //}
      
    }
}

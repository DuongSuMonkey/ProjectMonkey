﻿using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using System.Linq;
using System.IO;
using UnityEditor;
using System;
[System.Serializable]
public class DrawPolygonCollider : MonoBehaviour
{
    [SerializeField] private JsonColliderPath jsonPath;
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
    }
    public void GetPath()
    {
        jsonPath = GetComponent<JsonColliderPath>();
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
}




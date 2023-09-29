using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class JsonColliderBuilder : MonoBehaviour, IJsonColliderBuilder
{
    [SerializeField] private JsonPagePath jsonPath;
    [SerializeField] private string path = "";
    [SerializeField] private List<PolygonCollider2D> polygonCollider2D;
    [SerializeField] private List<Vector2> vertices = new List<Vector2>();
    [SerializeField] private TouchObject touchObject;
    [SerializeField] private IDrawCollider drawCollider;
    [SerializeField] private ISetWordByID setWordByID;
    private void Reset()
    {
        GetPathPage();
        GetTouchObjectPrefab();
        drawCollider = new DrawCollider(path, polygonCollider2D, vertices, touchObject);
        GetCollider();
        DrawCollider();
        setWordByID=new SetWordByID(path, polygonCollider2D);
        SetWordById();
    }
    public void GetPathPage()
    {
        jsonPath = GetComponent<JsonPagePath>();
        path = jsonPath.pathPage;
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
        drawCollider.DrawColliders(path, this);
    }
    public void SetWordById()
    {
        setWordByID.SetWordById(path);
    }
}


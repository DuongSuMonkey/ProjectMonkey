using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJsonColliderBuilder 
{
    void GetCollider();
    void DrawCollider();
    void GetWordByID(string fileName, PolygonCollider2D polygonCollider);
    void SetWordById();
    void GetPath();
}

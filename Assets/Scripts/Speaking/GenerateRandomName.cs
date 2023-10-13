using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomName : IGenerateRandomName
{
    public string GenerateRandomString()
    {
        char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        string randomString = "";
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            randomString += chars[Random.Range(0, chars.Length)];
        }

        return randomString;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetSyncData 
{
    List<string> GetSyncDataPaths(string pathPage, string syncDataPath, List<string> filesPath);
    void GetSyncDatas(string path);

}

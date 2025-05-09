using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Serialization.Json;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private string saveFilePath;
    private JsonData jsonData = new JsonData();

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log(saveFilePath);
        if (!File.Exists(saveFilePath)) File.Create(saveFilePath);
        LoadData();
        jsonData.levels ??= new List<LevelData>();
    }

    public void SaveData()
    {
        var json = JsonUtility.ToJson(jsonData);
        Debug.Log(json);
        Debug.Log(jsonData);
        Debug.Log(jsonData.levels);
        Debug.Log(jsonData.levels.Count);
        File.WriteAllText(saveFilePath, json);
    }
    
    public void LoadData()
    {
        string json = File.ReadAllText(saveFilePath);
        jsonData = JsonUtility.FromJson<JsonData>(json);
    }
    
    public void AddLevel(int id, int par, int hitCount)
    {
        jsonData.levels.Add(new LevelData{levelID = id, par = par, hitCount = hitCount});
    }

    public void ModifyLevel(int id, int? par = null, int? hitCount = null)
    {
        var search = jsonData.levels.FirstOrDefault(x => x.levelID == id);
        if (search != null)
        {
            if (par != null)
                search.par = par.Value;
            if (hitCount != null)
                search.hitCount = hitCount.Value;
        }
    }

    public (int levelID, int par, int hitCount)? ReadLevel(int id)
    {
        var search = jsonData.levels.FirstOrDefault(x => x.levelID == id);
        return search == null ? null : (search.levelID, search.par, search.hitCount);
    }
}

[Serializable]
class JsonData
{
    public List<LevelData> levels;
}
[Serializable]
class LevelData
{
    public int levelID;
    public int par;
    public int hitCount;
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Serialization.Json;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private readonly string saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");
    private JsonData jsonData = new JsonData();

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        var json = JsonUtility.ToJson(jsonData);
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

    public (int levelID, int par, int hitCount) ReadLevel(int id)
    {
        var search = jsonData.levels.FirstOrDefault(x => x.levelID == id);
        return search == null ? (0, 0, 0) : (search.levelID, search.par, search.hitCount);
    }
}

class JsonData
{
    public List<LevelData> levels;
}
class LevelData
{
    public int levelID;
    public int par;
    public int hitCount;
}
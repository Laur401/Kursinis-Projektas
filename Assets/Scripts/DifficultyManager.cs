using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public DifficultyLevel difficultyLevel;

    public void SetEasy(Boolean value)
    {
        if (value) difficultyLevel = DifficultyLevel.Easy;
    }
    public void SetMedium(Boolean value)
    {
        if (value) difficultyLevel = DifficultyLevel.Medium;
    }
    public void SetHard(Boolean value)
    {
        if (value) difficultyLevel = DifficultyLevel.Hard;
    }

    void OnDestroy()
    {
        PlayerPrefs.SetString("difficulty", difficultyLevel.ToString());
        PlayerPrefs.Save();
    }
    
}
public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}
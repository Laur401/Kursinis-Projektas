using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyApplier : MonoBehaviour
{
    private DifficultyLevel difficultyLevel;
    private void Start()
    {
        if (!Enum.TryParse(PlayerPrefs.GetString("difficulty"), out difficultyLevel))
            difficultyLevel = DifficultyLevel.Easy;

        if (difficultyLevel == DifficultyLevel.Easy) return;
        
        foreach (var o in GameObject.FindGameObjectsWithTag("PowerupUI"))
            o.SetActive(false);
        var powerups = FindObjectsByType<PowerupPickup>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        if (powerups.Length <= 0) return;
        foreach (var powerup in powerups)
        {
            powerup.gameObject.SetActive(false);
        }
        
        if (difficultyLevel == DifficultyLevel.Medium) return;
        
        foreach (var o in GameObject.FindGameObjectsWithTag("PlayerPointer"))
            o.SetActive(false);
    }
}

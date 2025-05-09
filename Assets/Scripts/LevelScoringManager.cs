using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScoringManager : MonoBehaviour
{
    private int _currentStroke = 0;
    private UIInfoElements _uiElements;
    private LevelVariables _levelVariables;
    private SaveDataManager _saveDataManager;

    private void Start()
    {
        _uiElements = FindAnyObjectByType<UIInfoElements>();
        _levelVariables = FindAnyObjectByType<LevelVariables>();
        _saveDataManager = FindAnyObjectByType<SaveDataManager>();
        
        _uiElements.SetHole(_levelVariables.hole);
        _uiElements.SetPar(_levelVariables.par);
        _uiElements.SetStroke(_currentStroke);
        
        _saveDataManager?.AddLevel(_levelVariables.hole, _levelVariables.par, _currentStroke);
    }

    public void AddStroke()
    {
        _currentStroke++;
        _uiElements.SetStroke(_currentStroke);
        _saveDataManager?.ModifyLevel(_levelVariables.hole, hitCount: _currentStroke);
    }

    private void OnDestroy()
    {
        _saveDataManager?.SaveData();
    }
}

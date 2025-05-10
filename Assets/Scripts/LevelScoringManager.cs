using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScoringManager : MonoBehaviour, IGameEventMessageTarget
{
    private int _currentStroke = 0;
    private UIInfoElements _uiElements;
    private LevelVariables _levelVariables;
    private SaveDataManager _saveDataManager;
    private ScorePanelManager _scorePanelManager;

    private void Start()
    {
        _uiElements = FindAnyObjectByType<UIInfoElements>();
        _levelVariables = FindAnyObjectByType<LevelVariables>();
        _saveDataManager = FindAnyObjectByType<SaveDataManager>();
        _scorePanelManager = FindAnyObjectByType<ScorePanelManager>();
        
        _scorePanelManager.LoadWholePanel(_saveDataManager.ReadLevel);
        
        if (_uiElements != null)
        {
            _uiElements?.SetHole(_levelVariables.hole);
            _uiElements?.SetPar(_levelVariables.par);
            _uiElements?.SetStroke(_currentStroke);
        }

        if (_levelVariables != null)
        {
            var levelData = _saveDataManager?.ReadLevel(_levelVariables.hole);
            if (levelData == null)
            {
                _saveDataManager?.AddLevel(_levelVariables.hole, _levelVariables.par, _currentStroke);
                levelData = _saveDataManager?.ReadLevel(_levelVariables.hole);
            }
            
            if (levelData == null) return;
            
            if (levelData.Value.par != _levelVariables.par) _saveDataManager?.ModifyLevel(_levelVariables.hole, par: _levelVariables.par);
            _scorePanelManager?.SetPar(levelData.Value.levelID, levelData.Value.par);
            _scorePanelManager?.SetStroke(levelData.Value.levelID, _currentStroke);
            _scorePanelManager?.SetScore(levelData.Value.levelID, _currentStroke - levelData.Value.par);
        }
    }

    public void AddStroke()
    {
        _currentStroke++;
        _uiElements.SetStroke(_currentStroke);
        _saveDataManager?.ModifyLevel(_levelVariables.hole, hitCount: _currentStroke);
        _scorePanelManager?.SetStroke(_levelVariables.hole, _currentStroke);
        _scorePanelManager?.SetScore(_levelVariables.hole, _currentStroke - _levelVariables.par);
    }

    public void OnSaveButtonPress() => SaveScoringData();
    
    public void OnNextLevelLoad() => SaveScoringData();

    private void SaveScoringData()
    {
        Debug.Log("Saving data");
        _saveDataManager?.SaveData();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScoringManager : MonoBehaviour
{
    private int _currentStroke = 0;
    private UIInfoElements _uiElements;
    private LevelVariables _levelVariables;

    private void Start()
    {
        _uiElements = FindAnyObjectByType<UIInfoElements>();
        _levelVariables = FindAnyObjectByType<LevelVariables>();
        _uiElements.SetHole(_levelVariables.hole);
        _uiElements.SetPar(_levelVariables.par);
        _uiElements.SetStroke(_currentStroke);
    }

    public void AddStroke()
    {
        _currentStroke++;
        _uiElements.SetStroke(_currentStroke);
    }
}

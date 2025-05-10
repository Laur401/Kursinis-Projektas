using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityProgressBar;


public class UIInfoElements : MonoBehaviour
{
    private TMP_Text strokeText;
    private TMP_Text parText;
    private TMP_Text holeText;
    private ProgressBar progressBar;
    private string test;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(new Scene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var textObjects = FindObjectsByType<TMP_Text>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (TMP_Text textObject in textObjects)
        {
            switch (textObject.gameObject.name)
            {
                case "Stroke":
                    strokeText = textObject;
                    break;
                case "Par":
                    parText = textObject;
                    break;
                case "Hole":
                    holeText = textObject;
                    break;
            }
        }
        var progressBars = FindObjectsByType<ProgressBar>(FindObjectsSortMode.None);
        foreach (var p in progressBars)
        {
            if (p.gameObject.name == "Strength Bar")
            {
                progressBar = p;
            }
        }
    }

    public void SetStroke(int stroke)
    {
        strokeText.text = $"Stroke {stroke}";
    }

    public void SetPar(int par)
    {
        parText.text = $"Par {par}";
    }
    
    public void SetHole(int hole)
    {
        holeText.text = $"Hole {hole}";
    }

    public void SetStrength(float value)
    {
        progressBar.Value = value;
    }
}

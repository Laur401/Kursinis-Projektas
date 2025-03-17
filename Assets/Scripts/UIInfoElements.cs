using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInfoElements : MonoBehaviour
{
    private TextMeshProUGUI strokeText;
    private TextMeshProUGUI parText;
    private TextMeshProUGUI holeText;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(new Scene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Replace with tags? Could be tag-list-wasteful though.
        var textObjects = FindAnyObjectByType<Canvas>().GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textObject in textObjects)
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
}

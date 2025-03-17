using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Play()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index+1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(index + 1);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }
}

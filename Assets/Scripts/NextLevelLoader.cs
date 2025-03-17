using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //TODO: Replace with direct player object testing
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(3);
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index+1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(index + 1);
    }
}


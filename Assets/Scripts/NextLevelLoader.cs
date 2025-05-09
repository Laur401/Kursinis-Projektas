using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
	
	[SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip winSound;
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            ExecuteEvents.Execute<IGameEventMessageTarget>
                (FindAnyObjectByType<LevelScoringManager>().gameObject, null, (handler, data) => handler.OnNextLevelLoad());
            StartCoroutine(LoadNextLevel());
        }
    }
    private IEnumerator LoadNextLevel()
    {
		//audioSource.volume = 0.4f;
		//audioSource.PlayOneShot(winSound); //skamba, bet geriau be pridėjimo
        
        yield return new WaitForSecondsRealtime(3);
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index+1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(index + 1);
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseInput;
    [SerializeField] private GameObject panel;
    [SerializeField] private List<GameObject> otherPanels;
    [SerializeField] private List<MonoBehaviour> scriptsToChangeState = new();
    private bool isPanelActive;
    private void Start()
    {
        isPanelActive = panel?.activeInHierarchy ?? false;
        pauseInput.action.Enable();
        pauseInput.action.performed += TogglePause;
    }

    private void TogglePause(InputAction.CallbackContext ctx)
    {
        Debug.Log("pause");
        switch (isPanelActive)
        {
            case true:
                isPanelActive = false;
                foreach (var p in otherPanels) p.SetActive(isPanelActive);
                Time.timeScale = 1;
                break;
            case false:
                isPanelActive = true;
                Time.timeScale = 0;
                break;
        }
        panel.SetActive(isPanelActive);
        
        foreach (var s in scriptsToChangeState) s.enabled = !isPanelActive;
    }

    private void OnDestroy()
    {
        pauseInput.action.performed -= TogglePause;
    }

    public void ResumeButton() => TogglePause(new InputAction.CallbackContext());

    public void MainMenuButton() => SceneManager.LoadSceneAsync(0);
    
    /*public void SaveButton() => ExecuteEvents.Execute<IGameEventMessageTarget>
    (FindAnyObjectByType<LevelScoringManager>().gameObject, null, (handler, data) => handler.OnSaveButtonPress());*/
}

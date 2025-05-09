using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PowerupButtonUnlocker : MonoBehaviour, IGameEventMessageTarget
{
    [SerializeField] private List<Button> UIButtons;
    [SerializeField] private List<PowerupButton> powerups;

    private void Start()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
    }
    
    public void OnPowerupCollection()
    {
        Debug.Log("OnPowerupCollection");
        int selection = UnityEngine.Random.Range(0, powerups.Count);
        foreach (var button in UIButtons)
        {
            if (button.IsInteractable()) continue;
            button.onClick?.AddListener(()=> { 
                powerups[selection].buttonClicked.Invoke(); 
                button.interactable = false;
                button.GetComponentInChildren<TMP_Text>().text = ""; 
            });
            button.interactable = true;
            button.GetComponentInChildren<TMP_Text>().text = powerups[selection].buttonText;
            break;
        }
    }
}

[Serializable]
public class PowerupButton
{
    [SerializeField] public string buttonText;
    [SerializeField] public Sprite buttonSprite;
    [SerializeField] public UnityEvent buttonClicked;
}
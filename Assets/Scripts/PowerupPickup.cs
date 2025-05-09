using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerupPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExecuteEvents.Execute<IGameEventMessageTarget>(other.gameObject, null, 
                (handler, data) => handler.OnPowerupCollection());
            gameObject.SetActive(false);
        }
    }
}

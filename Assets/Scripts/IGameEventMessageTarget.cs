using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IGameEventMessageTarget : IEventSystemHandler
{
    void OnBallHitInput() { } //Brackets make the method optional
    void OnBallHit() { }
    void OnBallLocationUpdate(Vector3 newLocation) { }
    void OnPowerupCollection() { }
    void OnMousePowerupEnable() { }
    void OnMousePowerupDisable() { }
    void OnSaveButtonPress() { }
    void OnNextLevelLoad() { }
}

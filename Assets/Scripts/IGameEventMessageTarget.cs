using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IGameEventMessageTarget : IEventSystemHandler
{
    void OnBallHit() { } //Brackets make the method optional
    void OnBallLocationUpdate(Vector3 newLocation) { }
}

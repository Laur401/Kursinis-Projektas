using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Powerups : MonoBehaviour, IGameEventMessageTarget
{
    [SerializeField] private bool rewindPowerup = false;
    [SerializeField] private bool jumpPowerup = false;
    [SerializeField] private bool holeMagnetPowerup = false;
    [SerializeField] private GameObject holeLocation;
    [SerializeField] private float magnetLength = 5;
    [SerializeField] private float magnetStrength = 10;
    [SerializeField] private bool teleportPowerup = false;
    [SerializeField] private Rigidbody rb;
    private Stack<Vector3> lastLocationStack = new();
    private bool ballHit = false;
    
    // Update is called once per frame
    void Update()
    {
        if (rewindPowerup) {RewindPowerup(); rewindPowerup = false;}
        if (jumpPowerup) {StartCoroutine(JumpPowerup()); jumpPowerup = false;}
        if (holeMagnetPowerup) {StartCoroutine(HoleMagnetPowerup()); holeMagnetPowerup = false;}
        if (teleportPowerup) {StartCoroutine(TeleportPowerup()); teleportPowerup = false;}
    }

    private void RewindPowerup()
    {
        if (lastLocationStack.Count <= 0) return;
        rb.gameObject.transform.position = lastLocationStack.Pop();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private IEnumerator JumpPowerup()
    {
        yield return new WaitUntil(()=>ballHit);
        ballHit = false;
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    private IEnumerator HoleMagnetPowerup()
    {
        float timer = 0;
        Vector3 direction = (holeLocation.transform.position - rb.gameObject.transform.position).normalized;
        while (timer < magnetLength)
        {
            timer += Time.deltaTime;
            rb.AddForce(direction * magnetStrength, ForceMode.Force);
            yield return null;
        }
    }

    private IEnumerator TeleportPowerup()
    {
        Vector3? location = null;
        ExecuteEvents.Execute<IGameEventMessageTarget>(rb.gameObject, null, 
            (handler, data) => handler.OnMousePowerupEnable());
        while (!ballHit || location == null)
        {
            location = MouseReader.CastMouseClickRay();
            yield return null;
        }
        rb.gameObject.transform.position = location.Value;
        ExecuteEvents.Execute<IGameEventMessageTarget>(rb.gameObject, null, 
            (handler, data) => handler.OnMousePowerupDisable());
    }

    public void OnBallHitInput() => ballHit = true;
    public void OnBallLocationUpdate(Vector3 newLocation) => lastLocationStack.Push(newLocation);

    private void LateUpdate()
    {
        ballHit = false;
    }

    public void OnRewindPowerupActivate() => rewindPowerup = true;
    public void OnJumpPowerupActivate() => jumpPowerup = true;
    public void OnHoleMagnetActivate() => holeMagnetPowerup = true;
    public void OnTeleportPowerupActivate() => teleportPowerup = true;
}

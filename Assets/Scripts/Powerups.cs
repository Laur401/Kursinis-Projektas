using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour, IGameEventMessageTarget
{
    [SerializeField] private bool rewindPowerup = false;
    [SerializeField] private bool jumpPowerup = false;
    [SerializeField] private bool holeMagnetPowerup = false;
    [SerializeField] private GameObject holeLocation;
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
        if (teleportPowerup) {TeleportPowerup(); teleportPowerup = false;}
    }

    private void RewindPowerup()
    {
        if (lastLocationStack.Count!=0)
            rb.gameObject.transform.position = lastLocationStack.Pop();
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
        while (timer < 5) //TODO: Replace with serialized value later
        {
            timer += Time.deltaTime;
            rb.AddForce(direction * 20, ForceMode.Force);
            yield return null;
        }
    }

    private void TeleportPowerup()
    {
        
    }

    public void OnBallHit() => ballHit = true;
    public void OnBallLocationUpdate(Vector3 newLocation) => lastLocationStack.Push(newLocation);

    public void OnRewindPowerupActivate() => rewindPowerup = true;
    public void OnJumpPowerupActivate() => jumpPowerup = true;
    public void OnHoleMagnetActivate() => holeMagnetPowerup = true;
    public void OnTeleportPowerupActivate() => teleportPowerup = true;
}

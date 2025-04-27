using UnityEngine;

public class BallKickSound : MonoBehaviour, IGameEventMessageTarget
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    
    public void OnBallHitInput() => audioSource.PlayOneShot(shootSound);
}
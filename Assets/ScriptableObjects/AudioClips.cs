using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableAudioClips", menuName = "AudioClipsScriptable")]

public class AudioClips : ScriptableObject
{
    [Header("AudioClips For Player")]
    public AudioClip Move;
    public AudioClip Jump;
    public AudioClip JumpDouble;
    public AudioClip JumpReturnToGround;
    public AudioClip Break;

    [Header("Mix")]
    public AudioClip CannonFire;
    public AudioClip CannonBallImpact;
    public AudioClip BarrelHiss;
    public AudioClip BarrelExplode;
    public AudioClip GetCoin;
    public AudioClip MenuOpen;
    public AudioClip GameQuit;
    public AudioClip GamePlay;
    public AudioClip CheckPointGood;
    public AudioClip CheckPointBad;
    public AudioClip CheckPointRespawn;
    public AudioClip Dart;

    [Header("Music")]
    public AudioClip Music;
    public AudioClip MusicMenu;
    public AudioClip DieJingle;

}

using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioSource _WalkAudioSource;
    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip sound, float volume = 1f)
    {
        _AudioSource.volume = volume;
        _AudioSource.PlayOneShot(sound);
    }
    public void PlaySoundRandomPitch(AudioClip sound, float volume = 1f)
    {
        _AudioSource.volume = volume;
        _AudioSource.pitch = Random.Range(.9f, 1.1f);
        _AudioSource.PlayOneShot(sound);
    }

    public void StartWalk()
    {
        if(_WalkAudioSource.isPlaying == false) _WalkAudioSource.Play();
    }

    public void StopWalk()
    {
        if (_WalkAudioSource.isPlaying) _WalkAudioSource.Stop();
    }
}

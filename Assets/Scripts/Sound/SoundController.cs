using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    [SerializeField] private AudioSource _AudioSource;
    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip sound)
    {
        _AudioSource.PlayOneShot(sound);
    }
    public void PlaySoundRandomPitch(AudioClip sound)
    {
        _AudioSource.pitch = Random.Range(.9f, 1.1f);
        _AudioSource.PlayOneShot(sound);
    }
}

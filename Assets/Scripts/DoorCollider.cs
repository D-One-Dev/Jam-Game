using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    public bool locked;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip doorSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!locked)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _animator.SetTrigger("Open");
                SoundController.instance.PlaySoundRandomPitch(doorSound, .1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!locked)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _animator.SetTrigger("Close");
                SoundController.instance.PlaySoundRandomPitch(doorSound, .1f);
            }
        }
    }
}

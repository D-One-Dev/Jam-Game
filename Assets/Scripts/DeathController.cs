using TMPro;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public static DeathController instance;
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text deathScreenText;
    [SerializeField] private SceneLoader _sceneLoader;
    void Start()
    {
        instance = this;
    }

    public void TriggerDeath(string deathText)
    {
        PlayerInteraction.instance.playerStatus = -1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreenText.text = deathText;
        _animator.SetTrigger("Death");
    }

    public void GoToMenu()
    {
        _sceneLoader.StartSceneLoading("Menu");
    }

    public void RestartDay()
    {
        _sceneLoader.StartSceneLoading("Gameplay");
    }
}
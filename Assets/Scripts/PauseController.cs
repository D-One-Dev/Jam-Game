using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private SceneLoader _sceneLoader;
    private Controls _controls;
    private bool isPaused;
    private void Awake()
    {
        _controls = new Controls();
        _controls.Gameplay.Esc.performed += ctx => PlayPause();
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }

    public void PlayPause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            settingsScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        _sceneLoader.StartSceneLoading("Menu");
    }
}

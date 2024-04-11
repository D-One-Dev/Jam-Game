using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        _sceneLoader.StartSceneLoading("Gameplay");
    }
}
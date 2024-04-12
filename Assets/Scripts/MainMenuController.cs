using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Button loadGameButton;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Day", 1) == 1) loadGameButton.interactable = false;
        else loadGameButton.interactable = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        PlayerPrefs.SetFloat("PlayerPosX", -10000f);
        PlayerPrefs.SetFloat("PlayerPosY", -10000f);
        PlayerPrefs.SetFloat("PlayerPosZ", -10000f);

        PlayerPrefs.SetFloat("PlayerRotY", -10000f);
        PlayerPrefs.SetFloat("PlayerRotX", -10000f);
        
        PlayerPrefs.SetInt("Day", 1);
        PlayerPrefs.Save();
        _sceneLoader.StartSceneLoading("Gameplay");
    }

    public void LoadGame()
    {
        _sceneLoader.StartSceneLoading("Gameplay");
    }
}
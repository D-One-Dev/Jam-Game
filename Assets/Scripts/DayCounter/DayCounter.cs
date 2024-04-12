using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private Animator daySceenAnim;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Transform player, cam;
    private bool roverGameCompleted = true, signalGameCompleted = true, repairGameCompleted = true;
    public static DayCounter Instance;
    public int currentDay = 1;
    private void Awake()
    {
        currentDay = PlayerPrefs.GetInt("Day", 1);
    }
    void Start()
    {
        Instance = this;
    }
    
    public void GoToNextDay()
    {
        if(roverGameCompleted && signalGameCompleted && repairGameCompleted)
        {
            PlayerPrefs.SetFloat("PlayerPosX", player.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ", player.position.z);

            PlayerPrefs.SetFloat("PlayerRotY", player.localEulerAngles.y);
            PlayerPrefs.SetFloat("PlayerRotX", cam.localEulerAngles.x);

            currentDay++;
            PlayerPrefs.SetInt("Day", currentDay);

            PlayerPrefs.Save();
            _sceneLoader.StartSceneLoading("Gameplay");
        }
    }

    public void ResetDay()
    {
        currentDay = 1;
        PlayerPrefs.DeleteKey("Day");
    }
}

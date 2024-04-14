using System;
using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private Animator daySceenAnim;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Transform player, cam;
    [SerializeField] private string[] day1Triggers;
    [SerializeField] private string[] day2Triggers;
    [SerializeField] private string[] day3Triggers;
    [SerializeField] private string[] day4Triggers;
    [SerializeField] private string[] day5Triggers;
    [SerializeField] private string[] day6Triggers;
    [SerializeField] private string[] day7Triggers;

    public static DayCounter Instance;
    public int currentDay = 1;

    public bool canSleep;
    private bool isSleeping;

    private void Awake()
    {
        Instance = this;
        currentDay = PlayerPrefs.GetInt("Day", 1);
    }
    void Start()
    {
        //Instance = this;
    }
    
    public void GoToNextDay()
    {
        if (!isSleeping)
        {
            //CheckSleep();
            if(canSleep)
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
                isSleeping = true;
            }
        }
    }

    public void ResetDay()
    {
        currentDay = 1;
        PlayerPrefs.DeleteKey("Day");
    }

    public void SetTrigger(string trigger)
    {
        switch (currentDay)
        {
            case 1:
                if(Array.IndexOf(day1Triggers, trigger) != -1)
                {
                    day1Triggers[Array.IndexOf(day1Triggers, trigger)] = null;
                }
                else
                {
                    Debug.LogError("Cannot find trigger " + trigger);
                }
                break;
            case 2:
                if (Array.IndexOf(day2Triggers, trigger) != -1)
                {
                    day2Triggers[Array.IndexOf(day2Triggers, trigger)] = null;
                }
                else
                {
                    Debug.LogError("Cannot find trigger " + trigger);
                }
                break;
            case 3:
                if (Array.IndexOf(day3Triggers, trigger) != -1)
                {
                    day3Triggers[Array.IndexOf(day3Triggers, trigger)] = null;
                }
                else
                {
                    Debug.LogError("Cannot find trigger " + trigger);
                }
                break;
            case 4:
                if (Array.IndexOf(day4Triggers, trigger) != -1)
                {
                    day4Triggers[Array.IndexOf(day4Triggers, trigger)] = null;
                }
                else
                {
                    Debug.LogError("Cannot find trigger " + trigger);
                }
                break;
            case 5:
                if (Array.IndexOf(day5Triggers, trigger) != -1)
                {
                    day5Triggers[Array.IndexOf(day5Triggers, trigger)] = null;
                }
                else
                {
                    Debug.LogError("Cannot find trigger " + trigger);
                }
                break;
            case 6:
                if (Array.IndexOf(day6Triggers, trigger) != -1)
                {
                    day6Triggers[Array.IndexOf(day6Triggers, trigger)] = null;
                }
                else
                {
                    Debug.LogError("Cannot find trigger " + trigger);
                }
                break;
            case 7:
                if (Array.IndexOf(day7Triggers, trigger) != -1)
                {
                    day7Triggers[Array.IndexOf(day7Triggers, trigger)] = null;
                }
                else
                {
                    Debug.LogError("Cannot find trigger " + trigger);
                }
                break;
            default:
                break;
        }

        CheckSleep();
    }

    private void CheckSleep()
    {
        bool flag = false;
        switch (currentDay)
        {
            case 1:
                foreach(string trigger in day1Triggers)
                {
                    if(trigger != null) flag = true;
                }
                break;
            case 2:
                foreach (string trigger in day2Triggers)
                {
                    if (trigger != null) flag = true;
                }
                break;
            case 3:
                foreach (string trigger in day3Triggers)
                {
                    if (trigger != null) flag = true;
                }
                break;
            case 4:
                foreach (string trigger in day4Triggers)
                {
                    if (trigger != null) flag = true;
                }
                break;
            case 5:
                foreach (string trigger in day5Triggers)
                {
                    if (trigger != null) flag = true;
                }
                break;
            case 6:
                foreach (string trigger in day6Triggers)
                {
                    if (trigger != null) flag = true;
                }
                break;
            case 7:
                foreach (string trigger in day7Triggers)
                {
                    if (trigger != null) flag = true;
                }
                break;
            default:
                break;
        }

        if (!flag)
        {
            canSleep = true;
        }
        else canSleep = false;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private TMP_Text resolutionText;
    [SerializeField] private TMP_Text fullscreenText;
    [SerializeField] private TMP_Text volumeText;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;
    private int resolution;
    private bool fullscreen;
    private int volume;
    void Start()
    {
        resolution = PlayerPrefs.GetInt("Resolution", 1);
        switch (PlayerPrefs.GetInt("Fullscreen", 1))
        {
            case 0:
                fullscreen = false;
                break;
            default:
                fullscreen = true;
                break;
        }
        volume = PlayerPrefs.GetInt("Volume", 50);
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 1;
        ApplyResolution();
        ApplySettings();
    }

    private void ApplyResolution()
    {
        switch (resolution)
        {
            case 0:
                Screen.SetResolution(1366, 768, fullscreen);
                resolutionText.text = "���������� ������: 1366x768";
                break;
            case 1:
                Screen.SetResolution(1920, 1080, fullscreen);
                resolutionText.text = "���������� ������: 1920x1080";
                break;
            case 2:
                Screen.SetResolution(2560, 1440, fullscreen);
                resolutionText.text = "���������� ������: 2560x1440";
                break;
            case 3:
                Screen.SetResolution(3840, 2160, fullscreen);
                resolutionText.text = "���������� ������: 3840x2160";
                break;
            default:
                Screen.SetResolution(1920, 1080, fullscreen);
                resolutionText.text = "���������� ������: 1920x1080";
                break;
        }

        fullscreenToggle.isOn = fullscreen;
        if (fullscreen) fullscreenText.text = "������������� �����: ���";
        else fullscreenText.text = "������������� �����: ����";

        PlayerPrefs.SetInt("Resolution", resolution);
        if(fullscreen) PlayerPrefs.SetInt("Fullscreen", 1);
        else PlayerPrefs.SetInt("Fullscreen", 0);
    }

    private void ApplySettings()
    {
        volumeSlider.value = volume;
        volumeText.text = "���������: " + volume;
        PlayerPrefs.SetInt("Volume", volume);
    }

    public void ChangeResolution(bool direction)
    {
        if (direction)
        {
            if (resolution <= 2) resolution++;
            else resolution = 0;
        }
        else
        {
            if(resolution > 0) resolution--;
            else resolution = 3;
        }

        ApplyResolution();
    }

    public void ChangeFullscreen()
    {
        fullscreen = fullscreenToggle.isOn;
        ApplyResolution();
    }

    public void ChangeVolume()
    {
        volume = (int)volumeSlider.value;
        ApplySettings();
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private TMP_Text resolutionText;
    [SerializeField] private TMP_Text fullscreenText;
    [SerializeField] private TMP_Text volumeText;
    [SerializeField] private TMP_Text sensText;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private TMP_Text qualityText;
    [SerializeField] private AudioMixerGroup _audioMixer;
    private int resolution;
    private int quality;
    private bool fullscreen;
    private int volume;
    private int sens;
    void Start()
    {
        resolution = PlayerPrefs.GetInt("Resolution", 1);
        quality = PlayerPrefs.GetInt("Quality", 3);
        switch (PlayerPrefs.GetInt("Fullscreen", 1))
        {
            case 0:
                fullscreen = false;
                break;
            default:
                fullscreen = true;
                break;
        }
        sens = PlayerPrefs.GetInt("Sens", 50);
        if (CameraLook.instance != null) CameraLook.instance.ChangeSens(sens);
        volume = PlayerPrefs.GetInt("Volume", 100);
        _audioMixer.audioMixer.SetFloat("Volume", Mathf.Lerp(-80f, 0f, volume / 100f));
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 1;
        ApplyResolution();
        ApplySettings();
        ApplyQuality();
    }

    private void ApplyResolution()
    {
        switch (resolution)
        {
            case 0:
                Screen.SetResolution(1366, 768, fullscreen);
                resolutionText.text = "Разрешение экрана: 1366x768";
                break;
            case 1:
                Screen.SetResolution(1920, 1080, fullscreen);
                resolutionText.text = "Разрешение экрана: 1920x1080";
                break;
            case 2:
                Screen.SetResolution(2560, 1440, fullscreen);
                resolutionText.text = "Разрешение экрана: 2560x1440";
                break;
            case 3:
                Screen.SetResolution(3840, 2160, fullscreen);
                resolutionText.text = "Разрешение экрана: 3840x2160";
                break;
            default:
                Screen.SetResolution(1920, 1080, fullscreen);
                resolutionText.text = "Разрешение экрана: 1920x1080";
                break;
        }

        fullscreenToggle.isOn = fullscreen;
        if (fullscreen) fullscreenText.text = "Полноэкранный режим: вкл";
        else fullscreenText.text = "Полноэкранный режим: выкл";

        PlayerPrefs.SetInt("Resolution", resolution);
        if(fullscreen) PlayerPrefs.SetInt("Fullscreen", 1);
        else PlayerPrefs.SetInt("Fullscreen", 0);
    }

    private void ApplySettings()
    {
        volumeSlider.value = volume;
        volumeText.text = "Громоксть: " + volume;
        PlayerPrefs.SetInt("Volume", volume);

        sensSlider.value = sens;
        sensText.text = "Чувствительность мыши: " + sens;
        PlayerPrefs.SetInt("Sens", sens);
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
        _audioMixer.audioMixer.SetFloat("Volume", Mathf.Lerp(-80f, 0f, volume/100f));
        ApplySettings();
    }

    public void ChangeMouseSens()
    {
        sens = (int)sensSlider.value;

        if(CameraLook.instance != null) CameraLook.instance.ChangeSens(sens);
        ApplySettings();
    }

    public void ChangeQuality(bool direction)
    {
        if (direction)
        {
            if (quality <= 2) quality++;
            else quality = 0;
        }
        else
        {
            if (quality > 0) quality--;
            else quality = 3;
        }
        PlayerPrefs.SetInt("Quality", quality);
        ApplyQuality();
    }

    private void ApplyQuality()
    {
        switch (quality)
        {
            case 0:
                qualityText.text = "Качество графики: минимальное";
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                qualityText.text = "Качество графики: среднее";
                QualitySettings.SetQualityLevel(2);
                break;
            case 2:
                qualityText.text = "Качество графики: высокое";
                QualitySettings.SetQualityLevel(4);
                break;
            case 3:
                qualityText.text = "Качество графики: ультра";
                QualitySettings.SetQualityLevel(5);
                break;
            default:
                qualityText.text = "Качество графики: минимальное";
                QualitySettings.SetQualityLevel(0);
                break;
        }

        QualitySettings.vSyncCount = 1;
    }
}
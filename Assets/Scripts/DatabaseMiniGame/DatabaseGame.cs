using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DatabaseGame : MonoBehaviour, IInteractable
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private RectTransform leftBorder, rightBorder;
    [SerializeField] private RectTransform[] greenZones;
    [SerializeField] private float cursorMoveSpeed;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private int lives;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private Animator _animator;

    [SerializeField] private AudioClip catchCorrect, catchWrong, gameWin, gameLoose;

    [SerializeField] private UnityEvent onCompleteGame;

    private int progress;
    private Controls _controls;
    private bool active;
    private void Awake()
    {
        _controls = new Controls();
        _controls.Gameplay.Space.performed += ctx => Catch();
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }

    private void FixedUpdate()
    {
        cursor.position += new Vector3(cursorMoveSpeed, 0f, 0f);
        if (cursor.localPosition.x < leftBorder.localPosition.x || cursor.localPosition.x > rightBorder.localPosition.x) cursorMoveSpeed *= -1;
    }

    private void Catch()
    {
        if(PlayerInteraction.instance.playerStatus == 1 && active)
        {
            RectTransform zone = CheckCollision();
            if (CheckCollision() != null)
            {
                zone.sizeDelta = new Vector2(zone.sizeDelta.x / 1.5f, zone.sizeDelta.y);
                progress++;
                progressBar.fillAmount = (float)progress / (greenZones.Length * 3);
                if (progress == greenZones.Length * 3)
                {
                    Debug.Log("Win");
                    _animator.SetTrigger("Win");
                    SoundController.instance.PlaySoundRandomPitch(gameWin);
                    
                    onCompleteGame?.Invoke();
                }
                else SoundController.instance.PlaySoundRandomPitch(catchCorrect);
            }
            else
            {
                lives--;
                livesText.text = "Попыток: " + lives;
                if (lives == 0)
                {
                    SoundController.instance.PlaySoundRandomPitch(gameLoose);
                    DeathController.instance.TriggerDeath("В ходе взлома вы активировали сигнализацию. Система охраны комплекса атаковала и убила вас");
                    Debug.Log("Loose");
                }
                else SoundController.instance.PlaySoundRandomPitch(catchWrong);
            }
        }
    }

    private RectTransform CheckCollision()
    {
        foreach (RectTransform zone in greenZones)
        {
            if (cursor.localPosition.x >= zone.localPosition.x - zone.rect.width && cursor.localPosition.x <= zone.localPosition.x + zone.rect.width)
            { 
                return zone;
            }
        }
        return null;
    }

    public void TurnOn()
    {
        active = true;
        gameUI.SetActive(true);
    }

    public void TurnOff()
    {
        active = false;
        gameUI.SetActive(false);
    }
}

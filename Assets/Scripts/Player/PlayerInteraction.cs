using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int interactDistance;
    [SerializeField] private LayerMask _lm;
    [SerializeField] private Image crosshair;
    [SerializeField] private Sprite crosshairWhite, crosshairGreen;
    public static PlayerInteraction instance;
    public int playerStatus = 0;
    private Controls _controls;
    public GameObject currentObject;
    bool canInteract;
    private void Awake()
    {
        instance = this;
        _controls = new Controls();
        _controls.Gameplay.Interact.performed += ctx => Interact();
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
        CheckInteraction();
    }
    private void CheckInteraction()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactDistance, _lm))
        {
            canInteract = true;
            crosshair.sprite = crosshairGreen;
            currentObject = hit.transform.gameObject;
        }
        else
        {
            canInteract = false;
            crosshair.sprite = crosshairWhite;
            currentObject = null;
        }
    }

    private void Interact()
    {
        if (canInteract)
        {
            switch (currentObject.tag)
            {
                case "Minigame":
                    if (playerStatus == 0)
                    {
                        playerStatus = 1;
                        currentObject.GetComponentInChildren<MinigameSelector>().TurnOn();
                        crosshair.enabled = false;
                    }
                    else
                    {
                        playerStatus = 0;
                        currentObject.GetComponentInChildren<MinigameSelector>().TurnOff();
                        crosshair.enabled = true;
                    }
                    break;
                case "Bed":
                    DayCounter.Instance.GoToNextDay();
                    break;
                default:
                    break;
            }
        }
    }
}

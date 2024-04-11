using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float mouseSens;
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;
    private Controls _controls;
    private void Awake()
    {
        _controls = new Controls();
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (PlayerInteraction.instance.playerStatus == 0)
        {
            Vector2 mouseDelta = _controls.Gameplay.MouseDelta.ReadValue<Vector2>();
            float mouseX = mouseDelta.x * Time.deltaTime * mouseSens;
            float mouseY = mouseDelta.y * Time.deltaTime * mouseSens;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}

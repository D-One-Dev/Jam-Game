using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] private float mouseSens;
    [SerializeField] private float smoothness;
    private Controls _controls;
    private Vector2 newRot;
    private Vector2 lerpedRot = Vector2.zero;
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
    private void Start()
    {
        newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y);
    }
    void FixedUpdate()
    {
        Vector2 mouseDelta = _controls.Gameplay.MouseDelta.ReadValue<Vector2>();
        float mouseX = mouseDelta.x * mouseSens;
        float mouseY = mouseDelta.y * mouseSens;
        newRot.x -= mouseY;
        newRot.y += mouseX;

        lerpedRot = new Vector2(Mathf.Lerp(lerpedRot.x, newRot.x, smoothness), Mathf.Lerp(lerpedRot.y, newRot.y, smoothness));

        transform.localRotation = Quaternion.Euler(lerpedRot.x, lerpedRot.y, 0);
    }
}
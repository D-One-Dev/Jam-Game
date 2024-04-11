using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float movementSpeed;
    private Controls _controls;
    private void Awake()
    {
        _controls = new Controls();
        //_controls.Gameplay.Interact.performed += ctx => Interact();
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }
    void Update()
    {
        Vector2 input = _controls.Gameplay.Movement.ReadValue<Vector2>();
        Vector3 movement = movementSpeed * Time.deltaTime * (input.x * transform.right + input.y * transform.forward);
        _characterController.Move(movement);
    }
}

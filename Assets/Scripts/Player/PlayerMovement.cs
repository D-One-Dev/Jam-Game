using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity;
    private Controls _controls;
    private float grav;
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
    void Update()
    {
        if(PlayerInteraction.instance.playerStatus == 0)
        {
            Vector2 input = _controls.Gameplay.Movement.ReadValue<Vector2>();
            Vector3 movement = movementSpeed * Time.deltaTime * (input.x * transform.right + input.y * transform.forward);
            _characterController.Move(movement);
        }
        if (_characterController.isGrounded) grav = 0;
        else grav += gravity;
        _characterController.Move(Vector3.up * grav * Time.deltaTime);
    }

}

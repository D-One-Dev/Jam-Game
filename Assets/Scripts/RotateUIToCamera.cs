using UnityEngine;

public class RotateUIToCamera : MonoBehaviour
{
    private Camera _camera;

    private void Start() => _camera = Camera.main;

    private void Update()
    {
        transform.eulerAngles = _camera.transform.eulerAngles;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int interactDistance;
    [SerializeField] private LayerMask _lm;
    public int playerStatus = 0;
    private Controls _controls;
    private void Awake()
    {
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

    private void Interact()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactDistance))
        {

        }
    }
}

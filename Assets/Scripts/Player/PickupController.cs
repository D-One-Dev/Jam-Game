using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("Pickup Settings")]
    
    [SerializeField] private Transform holdArea;

    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")] 
    
    [SerializeField] private float pickupRange = 5f;
    [SerializeField] private float pickupForce = 150f;
    [SerializeField] private float throwForce = 150f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }


        if (Input.GetMouseButtonDown(1) && heldObj != null)
        {
            ThrowObject();
        }
        else if (heldObj != null)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = holdArea.position - heldObj.transform.position;
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    private void PickupObject(GameObject pickObj)
    {
        pickObj.TryGetComponent<Rigidbody>(out var pickObjRB);
        
        if (pickObjRB != null)
        {
            heldObjRB = pickObjRB;
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            //heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }
    
    private void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        //heldObjRB.constraints = RigidbodyConstraints.None;

        heldObjRB.transform.parent = null;
        heldObj = null;
    }

    private void ThrowObject()
    {
        DropObject();
        heldObjRB.AddForce(transform.forward * throwForce);
    }
}
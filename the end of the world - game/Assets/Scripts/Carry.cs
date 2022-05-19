using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carry : MonoBehaviour
{
    [SerializeField] LayerMask pickupMask;
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform pickupTarget;
    [Space]
    [SerializeField] float pickupRange;
    Rigidbody currentObject;

    private void Start()
    {
        playerCamera = Camera.main;
    }
    void Update()
    {
        carryUp();
    }
    private void FixedUpdate()
    {
        setDistance();
    }
    void carryUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject)
            {
                currentObject.GetComponent<CapsuleCollider>().enabled = true;
                currentObject.GetComponent<BoxCollider>().enabled = true;
                currentObject.useGravity = true;
                currentObject = null;
                
                return;
            }

            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit, pickupRange, pickupMask))
            {
                currentObject = hit.rigidbody;
                currentObject.useGravity = false;
                currentObject.GetComponent<CapsuleCollider>().enabled = false;
                currentObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    void setDistance()
    {
        if (currentObject)
        {
            Vector3 directionToPoint = pickupTarget.position - currentObject.position;
            float distanceToPoint = directionToPoint.magnitude;

            currentObject.velocity = directionToPoint * 12f * distanceToPoint;
        }
    }
}

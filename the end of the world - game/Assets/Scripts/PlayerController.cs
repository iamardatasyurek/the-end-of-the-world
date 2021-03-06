using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody rb;
    Transform cameraTransform;
    bool grounded;
    float playerLenght;
    Collider capsule;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LoaderScreen loaderScreen;
    [SerializeField] GameObject flashlight;
    bool isOpen = false;
    
    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        capsule = GetComponent<CapsuleCollider>();
        playerLenght = capsule.transform.localScale.y;
    }

    void Update()
    {
        checkGrounded();
    }
    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    public void setVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }
    
    public void updateMouseLookX(Vector2 mouseDelta)
    {
        transform.Rotate(Vector3.up * mouseDelta.x );

    }
    public void updateMouseLookY(float vertical)
    {
        vertical = Math.Clamp(vertical,-40,60);
        cameraTransform.localEulerAngles = Vector3.right * vertical * 0.8f;
    }
    public void jump(float jumpForce)
    {
        if(grounded)
            rb.AddForce(transform.up * jumpForce);
    }
    void checkGrounded()
    {
        Ray ray = new Ray(transform.position,-transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, playerLenght, groundMask))
            grounded = true;
        else
            grounded = false;
    }

    public void diePlayer(int index)
    {
        loaderScreen.LoadScreenMenu(index);
    }
    public void useFlashlight()
    {
        if (isOpen == false)
        {
            flashlight.SetActive(true);          
            isOpen = true;
        }
        else
        {
            flashlight.SetActive(false);
            isOpen = false;
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] private float runSpeed = 10f, walkSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float jumpForce = 255f;
    private float verticalLookRotation;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput();
        mouseInput();
        jumpInput();
    }

    private void jumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerController.jump(jumpForce);
        }
    }

    private void mouseInput()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        mouseDelta *= mouseSensitivity;
        playerController.updateMouseLookX(mouseDelta);
        verticalLookRotation -= mouseDelta.y * mouseSensitivity;
        playerController.updateMouseLookY(verticalLookRotation);
    }

    private void movementInput()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        Vector3 velocity = moveInput.normalized;
        if (Input.GetKey(KeyCode.LeftShift))
            velocity *= runSpeed;
        else
            velocity *= walkSpeed;
        velocity = transform.TransformDirection(velocity);
        playerController.setVelocity(velocity);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity
{
    PlayerController playerController;
    [SerializeField] private float runSpeed = 10f, walkSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float jumpForce = 255f;
    private float verticalLookRotation;
    GunController gunController;

    bool haveGun = false;
    bool haveRedKey = false;
    bool haveBlueKey = false;   

    protected override void Start()
    {
        base.Start();
        playerController = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
    }

    void Update()
    {
        movementInput();
        mouseInput();
        jumpInput();
        weaponInput();
        weaponHiddenInput();
        flashlight();
        godMode();
        if (getHealth() <= 0)
            playerController.diePlayer(2);
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
        if (!PauseMenuController.isPaused)
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            mouseDelta *= mouseSensitivity;
            playerController.updateMouseLookX(mouseDelta);
            verticalLookRotation -= mouseDelta.y * mouseSensitivity;
            playerController.updateMouseLookY(verticalLookRotation);
        }       
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

    void weaponInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunController.shoot();
        }
    }

    void weaponHiddenInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            gunController.hiddenGun();
        }
    }

    void flashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerController.useFlashlight();
        }
    }

    void godMode()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            health = 1000;
            walkSpeed = 8;
            runSpeed = 16;
        }
    }

    public void risesHealth()
    {
        int randomHealth = UnityEngine.Random.Range(10, 20);
        if (health + randomHealth >= startingHealth)
            health = startingHealth;
        else
            health += randomHealth;
    }
    public float getHealth()
    {
        return health;
    }
    public float getStartingHealth()
    {
        return startingHealth;
    }
    public bool getHaveGun()
    {
        return haveGun;
    }
    public bool getHaveRedKey()
    {
        return haveRedKey;
    }
    public bool getHaveBlueKey()
    {
        return haveBlueKey;
    }
    public void setHaveGun(bool haveGun)
    {
        this.haveGun = haveGun;
    }
    public void setHaveRedKey(bool haveRedKey)
    {
        this.haveRedKey = haveRedKey;
    }
    public void setHaveBlueKey(bool haveBlueKey)
    {
        this.haveBlueKey = haveBlueKey;
    }

}

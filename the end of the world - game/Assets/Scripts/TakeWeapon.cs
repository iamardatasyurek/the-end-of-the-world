using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E)) {        
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
}

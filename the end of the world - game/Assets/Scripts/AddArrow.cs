using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddArrow : MonoBehaviour
{
    [SerializeField] Player player;
    Gun gun;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (player.getHaveGun() == true)
        {
            gun = player.GetComponentInChildren<Gun>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gun != null)
        {
            gun.addAmmo();
            Destroy(this.gameObject);
        }
    }
}

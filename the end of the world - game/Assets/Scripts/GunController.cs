using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun testgun;
    Gun equippedGun;
    [SerializeField] Transform weaponHold;
    [SerializeField] Player player;
    int count = 0;

    [SerializeField] GameObject ammoUI;
    

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (player.getHaveGun() == true && count == 0) 
        {           
            equipGun(testgun);
            count++;
        }
    }

    void equipGun(Gun newGun)
    {
        if (equippedGun != null)
            Destroy(equippedGun.gameObject);
        equippedGun = Instantiate(newGun, weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.shoot();
        }
    }

    public void hiddenGun()
    {
        if (equippedGun != null)
        {
            if(player.getHaveGun() == true)
            {
                equippedGun.hidden();
                player.setHaveGun(false);
                ammoUI.SetActive(false);
            }
            else
            {
                equippedGun.hidden();
                player.setHaveGun(true);
                ammoUI.SetActive(true);
            }          
        }       
    }
}

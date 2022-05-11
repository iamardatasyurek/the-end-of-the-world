using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Gun testgun;
    Gun equippedGun;
    [SerializeField] Transform weaponHold;

    private void Start()
    {
        equipGun(testgun);
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
}

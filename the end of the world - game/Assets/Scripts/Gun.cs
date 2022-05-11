using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] Projectile arrow;
    [SerializeField] float arrowSpeed = 10;
    [SerializeField] float delay = 2.5f;
    float nextShootTime;
    Animator animator;
    [SerializeField] AudioSource fireSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void shoot()
    {
        if (Time.time > nextShootTime) {
            animator.SetBool("Fire", true);
            nextShootTime = Time.time + delay;
            Projectile newArrow = Instantiate(arrow, muzzle.position, muzzle.rotation) as Projectile;           
            newArrow.setSpeed(arrowSpeed);
            fireSound.Play();
        }
        else animator.SetBool("Fire", false);
    }

  
}

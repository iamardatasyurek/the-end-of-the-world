using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] float startingHealth;
    float health;
    protected bool dead;
    protected Animator animator;

    protected virtual void Start()
    {
        health = startingHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeHit(float damage, Vector3 position)
    {
        //Kan cikti

        print(gameObject.name);
        health -= damage;
        if (health <= 0 && !dead) {
            dead = true;
            animator.SetBool("isDead", true);
            animator.SetBool("isAttack", false);
            Invoke("Die", 3.5f);
            
        }       
    }

    void Die()
    {
        health = 0;      
        GameObject.Destroy(this.gameObject);
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float damage = 1;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float lifeTime = 2;

    private void Start()
    {
        Collider[] initialColliders = Physics.OverlapSphere(transform.position, 0.3f, layerMask);
        if (initialColliders.Length > 0)
            onHitEnemy(initialColliders[0],transform.position);
        Destroy(this.gameObject, lifeTime);
    }
    void Update()
    {
        float distancePerFrame = speed * Time.deltaTime;
        checkCollision(distancePerFrame);

        transform.Translate(Vector3.forward * distancePerFrame, Space.Self);
        
    }

    private void checkCollision(float distancePerFrame)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, distancePerFrame, layerMask, QueryTriggerInteraction.Collide))
            onHitEnemy(hit.collider,hit.point);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    void onHitEnemy(Collider collider, Vector3 hitPoint)
    {
        IDamageable damageableObj = collider.GetComponent<IDamageable>();
        if(damageableObj != null)
            damageableObj.TakeHit(damage, hitPoint);
        Destroy(this.gameObject,1f);
    }
}

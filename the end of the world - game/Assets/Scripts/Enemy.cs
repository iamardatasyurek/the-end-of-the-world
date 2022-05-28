using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    [SerializeField] AudioSource enemySound;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform playerTransform;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;

    [SerializeField] Transform muzzle;
    float playerRadius;
    LineRenderer laser;
    [SerializeField] AudioSource laserSound;

    [SerializeField] GameObject healthBarUI;
    [SerializeField] Slider slider;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();       
    }

    protected override void Start()
    {
        base.Start();

        if(playerTransform != null)
        {
            playerRadius = playerTransform.GetComponent<CapsuleCollider>().radius;
        }

        laser = GetComponent<LineRenderer>();
        enemySound.Play();
        slider.value = sliderValue();     
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (dead)
        {
            playerInAttackRange = true;
            playerInSightRange = false;
            walkPointSet = false;
        }
        else
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }

        slider.value = sliderValue();
    }

    void Patroling() 
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        animator.SetBool("isAttack", false);
    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    void ChasePlayer() 
    {
        agent.SetDestination(playerTransform.position);
        animator.SetBool("isAttack", false);
    }
    void AttackPlayer() 
    {
        agent.SetDestination(transform.position); 
        transform.LookAt(playerTransform);

        if (!alreadyAttacked)
        {
            Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void Shoot()
    {
        float random = Random.Range(-2 * playerRadius, 2f * playerRadius);
        Vector3 randomVector3 = new Vector3(random, random, random);
        Vector3 directionToPlayer = ((playerTransform.position +randomVector3) - transform.position).normalized;
        
        Ray ray = new Ray(muzzle.position,directionToPlayer);
        RaycastHit hit;

        laser.SetPosition(0, muzzle.position);
        if (Physics.Raycast(ray, out hit, 50, whatIsPlayer))
        {
            IDamageable damageablePlayer = hit.collider.GetComponent<IDamageable>();
            if(damageablePlayer != null)
                damageablePlayer.TakeHit(4,hit.point);
            animator.SetBool("isAttack", true);
        }
        laser.SetPosition(1, muzzle.position + (directionToPlayer * 50));

        StartCoroutine(ShootEffect());     
    }

    IEnumerator ShootEffect()
    {
        laserSound.Play();
        laser.enabled = true;
        yield return timeBetweenAttacks;
        laser.enabled = false;
    }

    float sliderValue()
    {
        return health / startingHealth;
    }

}

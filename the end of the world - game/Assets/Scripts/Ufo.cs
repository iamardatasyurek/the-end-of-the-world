using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] Transform pathHolder;
    [SerializeField] float speed = 5;
    [SerializeField] float waitTime = 2;

    [SerializeField] Transform playerTransform;
    [SerializeField] LayerMask roofMask;
    [SerializeField] Light spotLight;
    [SerializeField] float viewDistance;
    float viewAngle;
    Color originColor;

    [SerializeField] Transform muzzle;
    float playerRadius;
    LineRenderer laser;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] float timeBetweenAttacks =10f;
    bool alreadyAttacked;

    State currentState;
    private void Start()
    {
        Vector3[] wayPoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = pathHolder.GetChild(i).position;
        }
        StartCoroutine(followPath(wayPoints));
        viewAngle = spotLight.spotAngle;
        playerTransform = GameObject.FindWithTag("Player").transform;
        originColor = spotLight.color;

        if (playerTransform != null)
        {
            playerRadius = playerTransform.GetComponent<CapsuleCollider>().radius;
        }
        laser = GetComponent<LineRenderer>();

        currentState = State.walking;
    }

    private void Update()
    {
        if (canSeePlayer())
        {
            spotLight.color = Color.red;
            AttackPlayer();
        }
        else
        {
            spotLight.color = originColor;
            laser.ResetBounds();
        }
    }

    bool canSeePlayer()
    {
        Vector3 distance = transform.position - playerTransform.position;
        float sqrDistance = distance.sqrMagnitude;
        if(sqrDistance < viewDistance * viewDistance)
        {
            Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
            float anglePlayerAndUfo = Vector3.Angle(-transform.up, dirToPlayer);
            if(anglePlayerAndUfo < viewAngle / 1.5f)
            {
                if(!Physics.Linecast(transform.position, playerTransform.position, roofMask))
                {
                    currentState = State.shooting;
                    //Vector3 directionToPlayer = ((playerTransform.position) - transform.position).normalized;
                    //print(playerTransform.position);
                    //print(transform.position);
                    //Debug.DrawLine(muzzle.position, playerTransform.position, Color.yellow); 
                    return true;
                }
            }
        }
        currentState = State.walking;




        

        return false;
        
    }

    IEnumerator followPath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];
        int targetWayPointIndex = 1;
        Vector3 targetWayPoint = wayPoints[targetWayPointIndex];
        while (currentState == State.walking)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            if (transform.position == targetWayPoint)
            {
                targetWayPointIndex = (targetWayPointIndex + 1)%wayPoints.Length;
                targetWayPoint = wayPoints[targetWayPointIndex];
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    void AttackPlayer()
    {
        if (currentState == State.shooting && !alreadyAttacked)
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

        float random = Random.Range(-5f * playerRadius, 5f * playerRadius);
        Vector3 randomVector3 = new Vector3(random, random, random);
        Vector3 playerAddRandom = playerTransform.position + randomVector3;   
        RaycastHit hit;

        laser.SetPosition(0, muzzle.position);
        if (Physics.Linecast(muzzle.position, playerAddRandom, out hit, whatIsPlayer))
        {
            print("vurulduk");
            print(hit.collider.gameObject.name);
            IDamageable damageablePlayer = hit.collider.GetComponent<IDamageable>();
            if (damageablePlayer != null)
                damageablePlayer.TakeHit(100, hit.point);
        }
        laser.SetPosition(1, playerAddRandom);

        StartCoroutine(ShootEffect());
    }

    IEnumerator ShootEffect()
    {
        laser.enabled = true;
        yield return timeBetweenAttacks;
        laser.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform t in pathHolder)
        {
            Gizmos.DrawSphere(t.position,2);
        }

        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, -transform.up* viewDistance);
    }

    enum State
    {
        walking,shooting
    }

}


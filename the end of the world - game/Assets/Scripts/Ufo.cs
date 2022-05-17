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
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator followPath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];
        int targetWayPointIndex = 1;
        Vector3 targetWayPoint = wayPoints[targetWayPointIndex];
        while (true)
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

        float random = Random.Range(-2f * playerRadius, 2f * playerRadius);
        Vector3 randomVector3 = new Vector3(random, random, random);
        Vector3 directionToPlayer = ((playerTransform.position + randomVector3) - transform.position).normalized;

        Ray ray = new Ray(muzzle.position, directionToPlayer);
        RaycastHit hit;

        laser.SetPosition(0, muzzle.position);
        if (Physics.Raycast(ray, out hit, 110, whatIsPlayer))
        {
            print("vurulduk");
            print(hit.collider.gameObject.name);
            IDamageable damageablePlayer = hit.collider.GetComponent<IDamageable>();
            if (damageablePlayer != null)
                damageablePlayer.TakeHit(100, hit.point);
        }
        laser.SetPosition(1, muzzle.position + (directionToPlayer * 110));

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



}

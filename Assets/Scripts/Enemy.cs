using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Vector3 min, max;
    Vector3 destination;
    bool playerDetected = false;
    public float playerDistanceDetection;
    public float visionAngle;
    public float playerAttackDistance;
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RandomDestination();
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!playerDetected && Vector3.Distance(transform.position, destination) < 1.5f)
            {
                RandomDestination();
            }
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    private void Update()
    {
        if (!playerDetected)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, playerDistanceDetection))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Vector3 vectorPlayer = player.position - transform.position;
                    if (Vector3.Angle(vectorPlayer.normalized, transform.forward) < visionAngle)
                    {
                        playerDetected = true;
                        StopAllCoroutines();
                        StartCoroutine(ChasePlayer());
                    }
                }
            }
        }
    }

    IEnumerator ChasePlayer()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.position) < playerAttackDistance)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                // Attack the player
            }
            else
            {
                GetComponent<NavMeshAgent>().isStopped = false;
                GetComponent<NavMeshAgent>().SetDestination(player.position);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void RandomDestination()
    {
        destination = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            StopAllCoroutines();
            StartCoroutine(ChasePlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
            StopAllCoroutines();
            StartCoroutine(Patrol());
        }
    }
}

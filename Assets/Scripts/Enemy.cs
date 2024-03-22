using System.Collections;
using System.Collections.Generic;
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

    private void Start() {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        RandomDestination();
        StartCoroutine(Patrol());
        StartCoroutine(Alert());
    }
    IEnumerator Alert()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.position) < playerDistanceDetection)
            {
                Vector3 vectorPlayer = player.position - transform.position;
                if (Vector3.Angle(vectorPlayer.normalized, transform.forward) < visionAngle)
                {
                    StopCoroutine(Patrol());
                    StartCoroutine(Attack());
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Attack()
    {
        StopCoroutine(Alert());
        while (true)
        {
            if (Vector3.Distance(transform.position, player.position) > playerDistanceDetection)
            {
                StartCoroutine(Patrol());
                StartCoroutine(Alert());
                StopCoroutine(Attack());
            }
            if (Vector3.Distance(transform.position, player.position) < playerAttackDistance)
            {
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
                GetComponent<NavMeshAgent>().velocity = Vector3.zero;
                // GetComponent<Animator>().SetBool("attack", true);
            }
            else
            {
                GetComponent<NavMeshAgent>().SetDestination(player.position);
                // GetComponent<Animator>().SetBool("attack", false);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void RandomDestination()
    {
        destination = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
        GetComponent<NavMeshAgent>().SetDestination(destination);
        // GetComponent<Animator>().SetFloat("velocity", 2);
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, destination) < 1.5f)
            {
                // GetComponent<Animator>().SetFloat("velocity", 0);
                yield return new WaitForSeconds(Random.Range(2, 7));
                RandomDestination();
            }
            yield return new WaitForEndOfFrame();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            StopCoroutine(Patrol());
            transform.LookAt(other.transform);
            GetComponent<NavMeshAgent>().SetDestination(other.transform.position);
            print("Player detected");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
            StartCoroutine(Patrol());
            print("Player lost");
        }
    }
}

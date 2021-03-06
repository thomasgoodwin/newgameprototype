using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject eyes;
    private GameObject[] players;
    [SerializeField]
    private float lookDamping = 2.2f;
    public float lookRadius = 10f;

    public Transform target;

    private float perceptionCheckMax = 3.0f;
    private float perceptionCheckCurrent = 3.0f;

    public float stoppingDistance = .5f;

    public Image crosshair;

    public float deaggroRange = 10f;

    CharacterStats characterStats;

    public bool HasLineOfSight(Transform target, out RaycastHit hitInfo)
    {
        Vector3 rayDirection = target.position - eyes.transform.position;
        LayerMask layer = 1 << LayerMask.NameToLayer("RaycastIgnore");
        if (Physics.Raycast(eyes.transform.position, rayDirection, out hitInfo, Mathf.Infinity, ~layer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerCollider");
        agent.stoppingDistance = stoppingDistance;
        characterStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // Perception Check
        if (perceptionCheckCurrent > 0.0f)
        {
            perceptionCheckCurrent -= Time.deltaTime;
            if (perceptionCheckCurrent < 0.0f)
            {
                perceptionCheckCurrent = 0.0f;
            }
        }
        if(perceptionCheckCurrent == 0.0f && target == null)
        {
            int perceptionRoll = Random.Range(1, 20);
            if (perceptionRoll >= 10)
            {
                LookForPlayers();
                perceptionCheckCurrent = perceptionCheckMax;
                Debug.Log("Enemy Perception Check Passed: " + perceptionRoll.ToString());
            }
            else 
            {
                perceptionCheckCurrent = perceptionCheckMax;
                Debug.Log("Enemy Perception Check Failed: " + perceptionRoll.ToString());
            }
        }
        if(target != null)
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lookDamping);
            float distanceFromTarget = Vector3.Distance(transform.position, target.position);
            if (distanceFromTarget > stoppingDistance)
            {
                agent.SetDestination(target.position);
            }

            RaycastHit hit;
            // deaggro check
            if (distanceFromTarget >= deaggroRange && HasLineOfSight(target, out hit))
            {
                target = null;
                EnemyManager.Instance.EnemyLeftCombat();
            }

        }
    }

    void LookForPlayers()
    {
        foreach (GameObject player in players)
        {
            RaycastHit hit;
            Vector3 rayDirection = player.transform.position - eyes.transform.position;
            if (HasLineOfSight(player.transform, out hit))
            {
                if (hit.transform.gameObject == player)
                {
                    EnemyManager.Instance.EnemyEnteredCombat();
                    Debug.DrawRay(eyes.transform.position, rayDirection * 500, Color.green, 0.01f);
                    target = player.transform;
                }
                else
                {
                    Debug.DrawRay(eyes.transform.position, rayDirection * 500, Color.red, 0.01f);
                }
            }
        }
    }
}

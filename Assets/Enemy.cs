using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public NavMeshAgent agent;
    public GameObject eyes;
    private GameObject[] players;
    public BarValue healthBar;
    [SerializeField]
    private float lookDamping = 2.2f;
    public float lookRadius = 10f;

    public Transform target;

    private float perceptionCheckMax = 3.0f;
    private float perceptionCheckCurrent = 3.0f;

    public float stoppingDistance = .5f; 

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerCollider");
        agent.stoppingDistance = stoppingDistance;
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
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

            if(Vector3.Distance(transform.position, target.position) > stoppingDistance)
            {
                agent.SetDestination(target.position);
            }
        }

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void LookForPlayers()
    {
        foreach (GameObject player in players)
        {
            RaycastHit hit;
            Vector3 rayDirection = player.transform.position - eyes.transform.position;
            LayerMask layer = 1 << LayerMask.NameToLayer("RaycastIgnore");
            if (Physics.Raycast(eyes.transform.position, rayDirection, out hit, Mathf.Infinity, ~layer))
            {
                if (hit.transform.gameObject == player)
                {
                    if (!GameManager.Instance.inCombat)
                    {
                        GameManager.Instance.StartCombat();
                    }
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetValue(currentHealth);
    }
}

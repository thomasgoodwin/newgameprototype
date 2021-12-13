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
    [SerializeField]
    private float lookDamping = 2.2f;

    private bool hasAggro = false;

    private float perceptionCheckMax = 3.0f;
    private float perceptionCheckCurrent = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerCollider");
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
        if(perceptionCheckCurrent == 0.0f && !hasAggro)
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
                    hasAggro = true;
                    var lookPos = player.transform.position - transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lookDamping);
                }
                else
                {
                    Debug.DrawRay(eyes.transform.position, rayDirection * 500, Color.red, 0.01f);
                }
            }
        }
    }
}

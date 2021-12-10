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

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerCollider");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject player in players)
        {
            RaycastHit hit;
            Vector3 rayDirection = player.transform.position - eyes.transform.position;
            LayerMask layer = 1 << LayerMask.NameToLayer("RaycastIgnore");
            if (Physics.Raycast(eyes.transform.position, rayDirection, out hit, Mathf.Infinity, ~layer))
            {
                
                if (hit.transform.gameObject == player)
                {
                    Debug.DrawRay(eyes.transform.position, rayDirection * 500, Color.green, 0.01f);

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

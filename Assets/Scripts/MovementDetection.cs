using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementDetection : MonoBehaviour
{
    NavMeshAgent agent;
    CharacterController controller;
    public bool CheckIsMoving()
    {
        if (controller != null)
        {
            if (controller.velocity.sqrMagnitude == 0.0f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (agent.velocity.sqrMagnitude == 0.0f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
    }
}

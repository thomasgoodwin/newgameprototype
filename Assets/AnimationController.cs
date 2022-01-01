using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    static Animator anim;
    MovementDetection movementDetection;

    public void PlayAbilityAnimation(string abilityTrigger)
    {
        anim.SetTrigger(abilityTrigger);
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        movementDetection = GetComponentInParent<MovementDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movementDetection.CheckIsMoving())
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}

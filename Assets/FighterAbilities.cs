using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAbilities
{

    public void NormalAttack(Transform player, Transform target)
    {
        Debug.Log("NORMAL ATTACK");
        if (target != null)
        {
            // do animation
            var lookPos = target.position - player.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            player.rotation = Quaternion.Slerp(player.rotation, rotation, Time.deltaTime * 2.2f);
            target.gameObject.GetComponent<Enemy>().TakeDamage(20);
        }
        else
        {
            // do animation to nothing

        }
    }
    public void FerociousDash(CharacterController controller, float dashForce = 5f)
    {
        controller.Move(controller.transform.forward * dashForce);
    }

}

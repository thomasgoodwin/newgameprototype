using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAbilities
{
    public void FerociousDash(CharacterController controller, float dashForce = 5f)
    {
        controller.Move(controller.transform.forward * dashForce);
    }

}

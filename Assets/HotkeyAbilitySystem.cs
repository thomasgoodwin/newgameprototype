using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyAbilitySystem : MonoBehaviour
{
    public CharacterController characterController;
    private FighterAbilities fighterAbilities;
    // Start is called before the first frame update
    void Start()
    {
        fighterAbilities = new FighterAbilities();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            fighterAbilities.FerociousDash(characterController);
        }
    }
}

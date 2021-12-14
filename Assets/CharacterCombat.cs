using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public CharacterController characterController;
    private FighterAbilities fighterAbilities;
    public Transform currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        fighterAbilities = new FighterAbilities();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fighterAbilities.NormalAttack(gameObject.transform, currentTarget);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "Enemy")
                {
                    if(currentTarget != null)
                    {
                        currentTarget.GetComponent<Enemy>().crosshair.enabled = false;
                    }
                    currentTarget = hitInfo.transform.gameObject.transform;
                    currentTarget.GetComponent<Enemy>().crosshair.enabled = true;
                }

            }

        }
    }
}

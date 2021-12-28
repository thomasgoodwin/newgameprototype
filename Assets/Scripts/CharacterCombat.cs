using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCombat : MonoBehaviour
{
    public CharacterController characterController;
    private FighterAbilities fighterAbilities;
    public Transform currentTarget;
    public ActionQueue actionQueue;

    // Start is called before the first frame update
    void Start()
    {
        fighterAbilities = new FighterAbilities();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gameManager = GameManager.Instance;

        if(currentTarget != null && gameManager.inCombat)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                actionQueue.AddAction(new CombatAction
                {
                    action = () => fighterAbilities.NormalAttack(transform, currentTarget),
                    actionIcon = SpriteBank.Instance.normalAttackIcon,
                    attacker = transform,
                    defender = currentTarget
                });
            }
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

        // if i can take my turn i take it
        if (gameManager.playerTurnTimerCurrent == 0.0f)
        {
            if (!actionQueue.isEmpty())
            {
                actionQueue.TakeTopAction();
            }
            gameManager.turnCounter++;
        }
    }

}

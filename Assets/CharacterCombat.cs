using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public CharacterController characterController;
    private FighterAbilities fighterAbilities;
    public Transform currentTarget;
    public Queue<CombatAction> combatAcitons;

    // Start is called before the first frame update
    void Start()
    {
        fighterAbilities = new FighterAbilities();
        combatAcitons = new Queue<CombatAction>();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gameManager = GameManager.Instance;

        if(currentTarget != null && gameManager.inCombat)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                combatAcitons.Enqueue(new CombatAction
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
            if (combatAcitons.Count > 0)
            {
                CombatAction topAction = combatAcitons.Dequeue();
                topAction.action();
                gameManager.SetPlayerTurnTimer();
            }
            gameManager.turnCounter++;
        }
    }

}

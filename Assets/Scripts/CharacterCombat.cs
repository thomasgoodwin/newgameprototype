using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCombat : MonoBehaviour
{
    public CharacterController characterController;
    public List<string> presetAbilities = new List<string>(); // so we can change character abilities in editor with tags
    public List<Ability> abilities = new List<Ability>(); // this is the actual list of abilities that might grow in game
    public Transform currentTarget;
    public ActionQueue actionQueue;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < presetAbilities.Count; i++)
        {
            // danger: this might be a shallow copy so it will not work with attacker and defender refs
            abilities.Add(AbilityBank.Instance.bank[presetAbilities[i]]); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gameManager = GameManager.Instance;

        if(currentTarget != null && gameManager.inCombat)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CombatAction newAction = new CombatAction
                {
                    ability = abilities[0], // danger: hard coded for basic attack
                    actionIcon = SpriteBank.Instance.normalAttackIcon
                };
                newAction.ability.m_attacker = transform;
                newAction.ability.m_defender = currentTarget.transform;
                actionQueue.AddAction(newAction);
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

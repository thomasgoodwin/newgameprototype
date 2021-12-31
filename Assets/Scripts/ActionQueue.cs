using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionQueue : MonoBehaviour
{
    public List<CombatAction> combatActions;
    public MovementDetection movementDetection;

    public void RemoveTopAction()
    {
        combatActions.RemoveAt(0);
        if (tag == "Player")
        {
            GetComponent<PCActionQueueUI>().RemoveTopAction();
        }
        else
        {
            // get enemy action queue UI
        }
    }

    public void ToggleUI()
    {
        if (tag == "Player")
        {
            GetComponent<PCActionQueueUI>().actionQueueUI.gameObject.SetActive(!GetComponent<PCActionQueueUI>().actionQueueUI.gameObject.activeInHierarchy);
        }
        else
        {
            // get enemy action queue UI
        }
    }

    public void AddAction(CombatAction action)
    {
        combatActions.Add(action);
        if(tag == "Player")
        {
            GetComponent<PCActionQueueUI>().AddAction(action);
        }
        else
        {
            // get enemy action queue UI
        }
    }

    public void TakeTopAction()
    {
        combatActions[0].ability.Activate();
        GameManager.Instance.SetPlayerTurnTimer();
        
    }

    public bool isEmpty()
    {
        if(combatActions.Count > 0)
            return false;
        else
            return true;
    }

    public void ClearActionQueue()
    {
        combatActions.Clear();
        if (tag == "Player")
        {
            GetComponent<PCActionQueueUI>().ClearActionQueue();
        }
        else
        {
            // get enemy action queue UI
        }
    }

    private void Awake()
    {
        combatActions = new List<CombatAction>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(combatActions.Count > 0)
        {
            if (combatActions[0].ability.m_defender == null || combatActions[0].ability.m_isFinished) // target of action is dead or action is done
            {
                RemoveTopAction();
            }
            else if (!movementDetection.CheckIsMoving())
            {
                combatActions[0].ability.Update();
            }
        }
        
    }
}

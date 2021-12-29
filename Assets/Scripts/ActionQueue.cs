using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionQueue : MonoBehaviour
{
    public List<CombatAction> combatActions;
    public Transform actionIconTemplate;
    public Canvas actionCanvas;
    public List<Transform> actionIcons;

    public int maxDisplayedActions = 4;

    public RectTransform currentActionBorder;

    private bool actionQueueisDirty = true;

    private CombatAction currentAction = null;

    public MovementDetection movementDetection;

    public void AddAction(CombatAction action)
    {
        combatActions.Add(action);
        actionQueueisDirty = true;
        Transform newIcon = Instantiate(actionIconTemplate);
        actionIcons.Add(newIcon);
        newIcon.parent = actionCanvas.transform;
        newIcon.GetChild(0).GetComponent<Image>().sprite = action.actionIcon;
        newIcon.gameObject.SetActive(false);
    }

    public void TakeTopAction()
    {
        currentAction = combatActions[0];
        currentAction.ability.Activate();
        combatActions.RemoveAt(0);
        GameManager.Instance.SetPlayerTurnTimer();
        Destroy(actionIcons[0].gameObject);
        actionIcons.RemoveAt(0);
        actionQueueisDirty = true;
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
        for (int i = 0; i < actionIcons.Count; i++)
        {
            Destroy(actionIcons[i].gameObject);
        }
        actionIcons.Clear();
        combatActions.Clear();
    }

    private void Awake()
    {
        combatActions = new List<CombatAction>();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(currentAction != null && !movementDetection.CheckIsMoving())
        {
            currentAction.ability.Update();
        }

        if(combatActions.Count > 0 && combatActions[0].ability.m_defender == null) // target of action is dead
        {
            Destroy(actionIcons[0].gameObject);
            combatActions.RemoveAt(0);
            actionIcons.RemoveAt(0);
            actionQueueisDirty = true;
        }

        if(actionQueueisDirty)
        {
            for (int i = 0; i < actionIcons.Count; i++)
            {
                if (i < maxDisplayedActions)
                {
                    Transform icon = actionIcons[i];
                    icon.GetComponent<RectTransform>().position = new Vector2(currentActionBorder.position.x - 110.0f * i,
                                                                              currentActionBorder.position.y);
                    icon.gameObject.SetActive(true);
                    if (i != 0)
                    {
                        icon.GetComponent<RectTransform>().localScale = new Vector3(.9f, .9f, .9f);
                    }
                }
                else
                {
                    Transform icon = actionIcons[i];
                    icon.gameObject.SetActive(false);
                }
            }
        }
    }
}

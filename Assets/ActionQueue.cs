using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionQueue : MonoBehaviour
{
    public List<CombatAction> combatAcitons;
    public Transform actionIconTemplate;
    public void AddAction(CombatAction action)
    {
        combatAcitons.Add(action);
    }

    public void TakeTopAction()
    {
        CombatAction topAction = combatAcitons[0];
        topAction.action();
        combatAcitons.Remove(topAction);
        GameManager.Instance.SetPlayerTurnTimer();
    }

    public bool isEmpty()
    {
        if(combatAcitons.Count > 0)
            return false;
        else
            return true;
    }

    private void Awake()
    {
        combatAcitons = new List<CombatAction>();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < combatAcitons.Count; i++)
        {
            CombatAction combatAction = combatAcitons[i];
            Transform newIcon = Instantiate(actionIconTemplate);
            newIcon.GetChild(0).GetComponent<Image>().sprite = combatAction.actionIcon;
            newIcon.gameObject.SetActive(true);
            RectTransform newIconRect = newIcon.GetComponent<RectTransform>();
            newIconRect.anchorMin = new Vector2(50f * i, 0f);
        }
    }
}

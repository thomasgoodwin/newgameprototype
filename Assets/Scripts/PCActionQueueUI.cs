using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCActionQueueUI : MonoBehaviour
{
    public List<Transform> actionIcons;
    public RectTransform currentActionBorder;
    public Transform actionIconTemplate;
    public Transform actionQueueUI;
    public Canvas actionCanvas;
    public int maxDisplayedActions = 4;
    private bool actionQueueisDirty = true;


    private void Awake()
    {
        actionIcons = new List<Transform>();
        actionQueueUI.gameObject.SetActive(false);
    }

    public void AddAction(CombatAction action)
    {
        Transform newIcon = Instantiate(actionIconTemplate);
        actionIcons.Add(newIcon);
        newIcon.parent = actionCanvas.transform;
        newIcon.GetChild(0).GetComponent<Image>().sprite = action.actionIcon;
        newIcon.gameObject.SetActive(false);
        actionQueueisDirty = true;
    }

    public void RemoveTopAction()
    {
        Destroy(actionIcons[0].gameObject);
        actionIcons.RemoveAt(0);
        actionQueueisDirty = true;
    }

    public void ClearActionQueue()
    {
        for (int i = 0; i < actionIcons.Count; i++)
        {
            Destroy(actionIcons[i].gameObject);
        }
        actionIcons.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actionQueueisDirty)
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

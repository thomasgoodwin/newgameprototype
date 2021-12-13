using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject CombatNotification;
    public GameObject PauseNotification;

    public bool inCombat = false;
    private void Awake()
    {
        Instance = this;
        CombatNotification.SetActive(false);
        PauseNotification.SetActive(false);
    }
    public void StartCombat()
    {
        inCombat = true;
        Time.timeScale = 0;
        PauseNotification.SetActive(true);
        CombatNotification.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PauseNotification.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PauseNotification.SetActive(false);

            }
        }
        
    }
}

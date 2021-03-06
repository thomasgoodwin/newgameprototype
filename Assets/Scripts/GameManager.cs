using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject CombatNotification;
    public GameObject PauseNotification;
    public ActionQueue actionQueue;
    public MovementDetection playerCharacter;

    public float playerTurnTimerCurrent = 0.0f; // danger: character turn timers should prob not be in game manager
    public float playerTurnTimerMax = 3.0f;

    public int partySize = 1;
    public int turnCounter = 0;

    public bool inCombat = false;

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayerTurnTimer()
    {
        playerTurnTimerCurrent = playerTurnTimerMax;
    }

    public void StartCombat()
    {
        inCombat = true;
        Time.timeScale = 0;
        ToggleCombatUI();
        SetPlayerTurnTimer();
    }

    public void EndCombat()
    {
        inCombat = false;
        CombatNotification.SetActive(!CombatNotification.activeSelf);
        actionQueue.ToggleUI();
    }

    public void ToggleCombatUI()
    {
        PauseNotification.SetActive(!PauseNotification.activeSelf);
        CombatNotification.SetActive(!CombatNotification.activeSelf);
        actionQueue.ToggleUI();
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
        if(turnCounter == partySize)
        {
            playerTurnTimerCurrent = playerTurnTimerMax;
            turnCounter = 0;
        }

        if(playerCharacter.CheckIsMoving())
        {
            playerTurnTimerCurrent = playerTurnTimerMax;
        }
        if (playerTurnTimerCurrent > 0.0f)
        {
            playerTurnTimerCurrent -= Time.deltaTime;
            if(playerTurnTimerCurrent < 0.0f)
            {
                playerTurnTimerCurrent = 0.0f;
            }
        }
    }
}

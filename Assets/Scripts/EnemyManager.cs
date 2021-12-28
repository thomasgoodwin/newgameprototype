using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    public int enemiesInCombat = 0;
    private void Awake()
    {
        Instance = this;
    }

    public void EnemyLeftCombat()
    {
        enemiesInCombat--;
        if(enemiesInCombat == 0)
        {
            GameManager.Instance.EndCombat();
        }
    }

    public void EnemyEnteredCombat()
    {
        if(enemiesInCombat == 0)
        {
            GameManager.Instance.StartCombat();
        }
        enemiesInCombat++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

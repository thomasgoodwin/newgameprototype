using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public BarValue healthBar;

    public int maxStamina = 100;
    public int currentStamina;
    public BarValue staminaBar;

    public int maxMana = 100;
    public int currentMana;
    public BarValue manaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);

        currentStamina = maxStamina;
        if(staminaBar)
        {
            staminaBar.SetMaxValue(maxHealth);
        }

        currentMana = maxMana;
        if(manaBar)
        {
            manaBar.SetMaxValue(maxHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            if(tag == "Enemy")
            {
                EnemyManager.Instance.EnemyLeftCombat();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetValue(currentHealth);
    }
}

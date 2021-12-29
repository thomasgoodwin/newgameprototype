using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public int currentXP = 0;
    public int currentLevel = 0;
    public int[] requiredXPForLevelUp;

    private void Awake()
    {
        requiredXPForLevelUp = new int[] { 0, 300, 900, 2700, 6500, 14000, 23000, 34000, 48000, 64000, 85000, 100000, 
                                            120000, 140000, 165000, 195000, 225000, 265000, 305000, 355000, 400000 };

    }

    private void Update()
    {
        if (currentXP >= requiredXPForLevelUp[currentLevel + 1])
        {
            currentLevel++; // level up
        }
    }
}

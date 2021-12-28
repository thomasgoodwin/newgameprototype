using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBank : MonoBehaviour
{
    public static SpriteBank Instance { get; private set; }

    public Sprite normalAttackIcon;

    
    private void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

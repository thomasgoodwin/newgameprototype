using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ability // all child abilities must init attacker and defender
{
    // initialize the ability
    virtual public void Activate()
    {

    }
    // if the ability needs to be ticked 
    virtual public void Update()
    {

    }
    // if the ability needs to do something when its over (call at the end of overriden funciton)
    virtual public void Deactivate() // todo: currently isnt being called due to current archecture
    {
        m_attacker = null;
        m_defender = null;
    }
    public Transform m_attacker = null;
    public Transform m_defender = null;
    public float m_baseCastingSpeed;
}

public class MutilateAbiltiy : Ability
{
    public MutilateAbiltiy(Transform attacker, Transform defender)
    {
        m_baseCastingSpeed = 15.0f;
        m_attacker = attacker;
        m_defender = defender;
    }
    override public void Activate()
    {
        m_defender.gameObject.GetComponent<CharacterStats>().TakeDamage(20);
    }
    override public void Update()
    {
        var lookPos = m_defender.position - m_attacker.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        m_attacker.rotation = Quaternion.Slerp(m_attacker.rotation, rotation, Time.deltaTime * 2.2f);
    }
    override public void Deactivate()
    {
        base.Deactivate();
    }
    
}



public class AbilityBank : MonoBehaviour
{
    public Dictionary<string, Ability> bank = new Dictionary<string, Ability>();
    public static AbilityBank Instance { get; private set; }
    private void Awake()
    {
        Instance = this;

        MutilateAbiltiy mutilateAbiltiy = new MutilateAbiltiy(null, null);
        bank.Add("mutilate_ability", mutilateAbiltiy);


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

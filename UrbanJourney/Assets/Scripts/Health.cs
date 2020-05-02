using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float HP;
    [SerializeField] bool isDead;
    public UnityEvent onDeath;
    public UnityEvent onTakeDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(float points)
    {
        HP = points;
    }

    void Die()
    {
        onDeath.Invoke();
    }

    public void GiveDamage(float dmg)
    {
        if(!isDead)
        {
            HP = HP - dmg;
            if(HP <= 0)
            {
                Die();
            }
        }
        
    }
}

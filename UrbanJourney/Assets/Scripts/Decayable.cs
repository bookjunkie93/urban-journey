using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decayable : MonoBehaviour
{
    Timescale timer;
    Health health;
    [SerializeField] DecayableType type;
    int lastTick;


    void Awake()
    {
        timer = GameObject.FindObjectOfType<Timescale>();
        health = GetComponent<Health>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(health != null)
        {
            health.Init(type.HP);
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        if(timer != null)
        {
            if(timer.GetTick() > lastTick)
            {
                lastTick = timer.GetTick();
                float damage = type.CalculateDecay(timer.GetScale());
                health.GiveDamage(damage);           
               
            }
        }
    }
}

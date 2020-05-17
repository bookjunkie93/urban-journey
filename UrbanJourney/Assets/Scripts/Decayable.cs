using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Health))]
public class Decayable : MonoBehaviour
{
    Timescale timer;
    Health health;
    [SerializeField] DecayableType type;
    public bool triggerOnTouch;
    [SerializeField]Collider2D touchTrigger;
    int lastTick;
    bool decayStarted;


    void Awake()
    {
        timer = GameObject.FindObjectOfType<Timescale>();
        health = GetComponent<Health>();
        if(touchTrigger == null)
        {
            touchTrigger = GetComponent<Collider2D>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(health != null)
        {
            health.Init(type.HP);
        }
    }

    /// <summary>
    /// If decay is not continuous for this object, trigger it
    /// </summary>
    public void TriggerDecay()
    {
        if(triggerOnTouch)
        {
            decayStarted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerOnTouch && !decayStarted) {return;}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Health))]
public class Decayable : MonoBehaviour
{
    Timescale timer;
    Health health;
    [SerializeField] DecayableType type;
    public bool touchTrigger;
    int lastTick;
    bool decayStarted;


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(touchTrigger && collision.gameObject.tag.Equals("Player"))
        {
            decayStarted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(touchTrigger && !decayStarted) {return;}
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

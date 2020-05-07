using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float cooldown = 5f;
    [SerializeField] bool useTicks;
    [SerializeField] bool touchTrigger;
    Timescale timer;
    GameObject instance;
    float lastTick;
    float timeElapsed =0;

    void Awake()
    {
        timer = GameObject.FindObjectOfType<Timescale>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            Spawn();
        }

    }

    private void Spawn()
    {
        instance = Instantiate(prefab, transform) as GameObject;
        if (touchTrigger)
        {
            Decayable dec = instance.GetComponent<Decayable>();
            if (dec != null)
            {
                dec.touchTrigger = true;
            }
        }
    }

    void updateTicks()
	{
        if (lastTick < timer.GetTick())
        {
            timeElapsed++;
        }
        if (timeElapsed >= cooldown)
        {
            timeElapsed = 0;
            Spawn();
        }
        lastTick = timer.GetTick();
    }

    void updateTime()
	{
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= cooldown)
        {
            timeElapsed = 0;
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(instance == null)
        {
            if(useTicks)
			{
                updateTicks();   
			}
			else
			{
                updateTime();   
			}
            
        }
        
    }
}

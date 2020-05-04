using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float cooldown = 5f;
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
            instance = Instantiate(prefab, transform) as GameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(instance == null)
        {
            if(lastTick < timer.GetTick())
            {
                timeElapsed++;
            }
            if(timeElapsed >= cooldown)
            {
                timeElapsed = 0;
                instance = Instantiate(prefab, transform) as GameObject;
            }
            lastTick = timer.GetTick();
        }
        
    }
}

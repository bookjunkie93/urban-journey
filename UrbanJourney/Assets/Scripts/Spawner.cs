using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] float cooldown = 5f;
    [SerializeField] bool useTicks;
    [SerializeField] bool touchTrigger;
    [SerializeField] List<TriggerMass> triggerMasses;
    Timescale timer;
    GameObject instance;
    [SerializeField] List<GameObject> instances;
    float lastTick;
    float timeElapsed =0;

    void Awake()
    {
        timer = GameObject.FindObjectOfType<Timescale>();
        instances = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if ((instance == null) && (prefab != null))
        {
            instance = SpawnSingle(prefab);
        }
        else if (instances.Count == 0 && prefabs != null)
        {
            SpawnMultiple();
        }
    }

    private GameObject SpawnSingle(GameObject i_prefab)
    {
        GameObject newInstance = Instantiate(i_prefab, transform) as GameObject;
        Decayable dec = newInstance.GetComponent<Decayable>();
        if (dec != null)
        {
            if (touchTrigger)
            {
                dec.triggerOnTouch = true;
                if(triggerMasses != null)
                {
                    foreach (TriggerMass mass in triggerMasses)
                    {
                        mass.onTrigger.AddListener(dec.TriggerDecay);
                    }
                }
                    
            }
            else
            {
                dec.triggerOnTouch = false;
            }            
        }
        return newInstance;
    }

    void SpawnMultiple()
    {
        foreach (GameObject nextPrefab in prefabs)
        {
            instances.Add(SpawnSingle(nextPrefab));
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

    void pruneList()
    {
        instances.RemoveAll(item => item == null);
    }
    // Update is called once per frame
    void Update()
    {
        pruneList();
        if(instance == null && (instances == null ||instances.Count == 0))
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

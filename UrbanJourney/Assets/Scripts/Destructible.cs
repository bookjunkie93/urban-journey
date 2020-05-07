using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(health != null)
        {
            health.onDeath.AddListener(Break);
        }
    }

    public void Break()
    {
        Debug.Log(string.Format("{0} Broke!", gameObject.name));
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimescaleController : MonoBehaviour
{
    Timescale timer;
    [SerializeField] float m_rateOfChange =0.1f;

    void Awake()
    {
        timer = GetComponent<Timescale>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("SlowTime"))
        {
            float newScale = timer.GetScale() - (Time.deltaTime * m_rateOfChange);
            timer.SetTickScale(newScale);
        }

        if(Input.GetButton("SpeedTime"))
        {
            float newScale = timer.GetScale() + (Time.deltaTime * m_rateOfChange);
            timer.SetTickScale(newScale);
        }

        if(Input.GetButton("ResetTime"))
        {
            timer.SetTickScale(1f);
        }
        
    }
}

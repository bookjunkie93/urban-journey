using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimescaleHUD : MonoBehaviour
{
    [SerializeField] Text currentTick;
    [SerializeField] Text currentScale;
    [SerializeField] Timescale timer;
    [SerializeField] float scaleInterval = 0.1f;
    void Awake()
    {
        timer = GameObject.FindObjectOfType<Timescale>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Increment the timescale by the current scaleInterval
    /// </summary>
    public void IncrementScale()
    {
        timer.SetTickScale(timer.GetScale() + scaleInterval);
    }

    /// <summary>
    /// Decrement the timescale by the current scaleInterval
    /// </summary>
    public void DecrementScale()
    {
        timer.SetTickScale(timer.GetScale() - scaleInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer != null)
        {
            currentTick.text= string.Format("Tick: {0}",  timer.GetTick().ToString());
            currentScale.text= string.Format("Scale: {0}x", timer.GetScale().ToString("F2"));
        }

    }
}

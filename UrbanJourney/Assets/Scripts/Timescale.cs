using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monobehaviour that keeps track of time and modifies the game's
/// timescale
/// </summary>
public class Timescale : MonoBehaviour
{
    [SerializeField] int nTick = 0;
    [SerializeField] float fScale = 1f; //ticks/second
    float elapsedTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetTick()
    {
        return nTick;
    }

    public float GetScale()
    {
        return fScale;
    }

    public void SetTickScale(float i_newScale)
    {
        fScale = i_newScale;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime * fScale >= 1)
        {
            elapsedTime = 0;
            nTick++;
        }
    }
}

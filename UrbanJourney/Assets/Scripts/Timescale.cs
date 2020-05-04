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
    [Range(0.1f, 2f)][SerializeField] float fScale = 1f; //ticks/second
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
        fScale = Mathf.Clamp(i_newScale, 0.1f, 2f);
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

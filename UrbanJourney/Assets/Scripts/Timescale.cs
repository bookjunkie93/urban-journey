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
    float duration = 10f;
    float cooldown = 3f;
    float powerTime = 0f;
    bool onCooldown;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTick();
        UpdateCooldown();
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
        if(onCooldown) {return;} //Can't mess with time while on cooldown!
        fScale = Mathf.Clamp(i_newScale, 0.1f, 2f);
    }

    private void UpdateTick()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime * fScale >= 1)
        {
            elapsedTime = 0;
            nTick++;
        }
    }

    private void UpdateCooldown()
    {
        powerTime += Time.deltaTime;
        if (fScale != 1 && powerTime >= duration)
        {
            SetTickScale(1.00f);
            onCooldown = true;
            powerTime = 0;
        }
        else if (onCooldown && powerTime >= cooldown)
        {
            onCooldown = false;
            powerTime = 0;
        }
        else if (!onCooldown && fScale == 1.00f)
        {
            powerTime = 0;
        }
    }
}

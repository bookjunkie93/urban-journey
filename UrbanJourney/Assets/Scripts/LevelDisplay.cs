using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;

public class LevelDisplay : MonoBehaviour
{
    BaseStats playerStats;
    Text playerLevelValue;
    void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        playerLevelValue = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLevelValue != null)
        {
            //playerLevelValue.text = string.Format("{0}", playerStats.GetLevel());
        }
    }
}

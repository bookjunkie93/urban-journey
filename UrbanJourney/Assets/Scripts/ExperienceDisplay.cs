using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;

public class ExperienceDisplay : MonoBehaviour
{
    Experience playerExp;
    Text playerExpValue;
    void Awake()
    {
        playerExp = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        playerExpValue = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerExpValue != null)
        {
            playerExpValue.text = string.Format("{0}", playerExp.GetPoints());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DecayableType", menuName = "Custom Assets/DecayableType")]
public class DecayableType : ScriptableObject
{
    public float HP;
    public Sprite[] sprites;
    public float decayRate;
    public bool reverseDecay;

    public float CalculateDecay(float CurrentScale)
    {
        if(reverseDecay)
        {
            return decayRate/CurrentScale;
        }
        else
        {
            return decayRate * CurrentScale;
        }
    }

}

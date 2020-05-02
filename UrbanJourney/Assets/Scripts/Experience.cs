using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using UnityEngine.Events;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        private float currentExp;
        public UnityEvent OnExperienceChanged = new UnityEvent();
       
        public void AddExperience(float exp)
        {
            currentExp += exp;
            OnExperienceChanged.Invoke();
        }

        public float GetPoints ()
        {
            return currentExp;
        }

       
        public object CaptureState()
        {
           return currentExp;
        }

        public void RestoreState(object state)
        {
            currentExp = (float)state;
        }

    }
}
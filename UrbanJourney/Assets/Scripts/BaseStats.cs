using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Saving;
using GameDevTV.Utils;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression Progression = null;
        [SerializeField] bool useModifiers = false;
        Experience exp;
        public UnityEvent onLevelUp = new UnityEvent();
        //LazyValue<int> currentLevel;

        void Awake()
        {
            exp = GetComponent<Experience>();
            //currentLevel = new LazyValue<int>(CalculateLevel);

        }

        private void OnEnable()
        {
            if (exp != null)
            {
            }
        }

        private void OnDisable()
        {
            if(exp != null)
            {
            }
        }

        private void Start()
        {
            //currentLevel.ForceInit();        
        }


        public float GetStat(Stat stat)
        {
            if (Progression == null)
            {
                throw new System.MissingMemberException("There is no Progression object attached to this component!");
            }
            float rawStat = Progression.GetStatValueAtLevel(stat, characterClass, 1);
            if(useModifiers)
            {
                float additiveStat = rawStat + GetAdditiveModifiers(stat);
                float finalStat = additiveStat + (additiveStat * GetPercentageModifiers(stat));
                return finalStat;
            }
            return rawStat;
        }

        public float GetAdditiveModifiers(Stat stat)
        {
            float sum = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetAdditiveModifier(stat))
                {
                    sum += modifier;
                }
            }
            return sum;
        }

        public float GetPercentageModifiers(Stat stat)
        {
            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetPercentageModifier(stat))
                {
                    total += modifier;
                }
            }
            return total/100;
        }

        //private int CalculateLevel()
        //{
        //    Experience experience = GetComponent<Experience>();
        //    if (experience == null) return startingLevel;
        //    float currentExp = experience.GetPoints();
        //    int penultimateLevel = Progression.GetLevels(1, characterClass);
        //    for (int i = 1; i <= penultimateLevel; i++)
        //    {
        //        float threshold = Progression.GetStatValueAtLevel(1, characterClass, i);
        //        if (currentExp >= threshold)
        //        {
        //            continue;
        //        }
        //        return i;
        //    }
        //    return penultimateLevel + 1;
        //}
        
        //public int GetLevel()
        //{
        //    if(currentLevel.value < 1)
        //    {
        //        currentLevel.value = CalculateLevel();
        //    }
        //    return currentLevel.value;
        //}

        private void Update()
        {
           
        }
    }
}


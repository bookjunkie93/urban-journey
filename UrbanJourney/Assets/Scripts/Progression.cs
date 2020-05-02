using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Generate Progression File", order = 0)]
    public class Progression : ScriptableObject
    {
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable;
        [SerializeField] ClassProgressionData[] classProgessions;

        const float defaultStatValue = 0f;

        [System.Serializable]
        public struct ClassProgressionData
        {
            public CharacterClass classType;
            public ProgressionStat[] statList;
        }
        [System.Serializable]
        public struct ProgressionStat
        {
            public Stat statType;
            public float[] levels;
        }

        public int GetLevels (Stat targetStat, CharacterClass @class)
        {
            BuildLookup();
            if(lookupTable.ContainsKey(@class))
            {
                if(lookupTable[@class].ContainsKey(targetStat))
                {
                    return lookupTable[@class][targetStat].Length;
                }
            }
            return 0;
        }

        public float GetStatValueAtLevel(Stat targetStat, CharacterClass @class, int targetLevel)
        {
           
            BuildLookup();

            
            if(lookupTable.ContainsKey(@class))
            {
                if(lookupTable[@class].ContainsKey(targetStat))
                {
                    float[] statLevels = lookupTable[@class][targetStat];
                    if (targetLevel <= statLevels.Length)
                    {
                        return statLevels[targetLevel - 1];
                    }
                    else
                    {
                        int maxValue = statLevels.Length - 1;
                        if (maxValue > 0)
                        {                        
                            Debug.LogWarningFormat("Class {0} has no stat of type: {1} set for level {2}, defaulting to level {3}", @class, targetStat, targetLevel, maxValue);
                            return statLevels[maxValue];
                        }
                    }
                }
                Debug.LogWarningFormat("Class{0} has no data for stat {1}, using default value", @class, targetStat);
                return defaultStatValue;
            }
            Debug.LogWarningFormat("No data found for Class {0}, using default value", @class);
            return defaultStatValue;
           
        }

        private void BuildLookup()
        {
            if(lookupTable != null) return;
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach(ClassProgressionData classData in classProgessions)
            {
                if(lookupTable.ContainsKey(classData.classType)) continue;
                Dictionary<Stat, float[]> classLookup = new Dictionary<Stat, float[]>();
                foreach(ProgressionStat stat in classData.statList)
                {
                    if(classLookup.ContainsKey(stat.statType)) continue;
                    classLookup.Add(stat.statType, stat.levels);
                }
                lookupTable.Add(classData.classType, classLookup);
            }
        }
       
    }
}

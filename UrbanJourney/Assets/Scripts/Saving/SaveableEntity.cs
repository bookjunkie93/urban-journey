using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();
        // Override this in the inspector to allow a prefab to share state between instances across scenes
        [SerializeField] string uniqueIdentifier = "";
        #region MonoBehaviour Methods

        void Awake()
        {
        }

        #if UNITY_EDITOR
        void Update()
        {
            //ensure that we have a unique GUID within and across scenes
            if(!Application.IsPlaying(gameObject) && !string.IsNullOrEmpty(gameObject.scene.path))
            {
                SerializedObject @object = new SerializedObject(this);
                SerializedProperty property = @object.FindProperty("uniqueIdentifier");
                if(string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
                {
                    property.stringValue = System.Guid.NewGuid().ToString();
                    @object.ApplyModifiedProperties();
                    globalLookup.Add(property.stringValue, this);
                }            
            }
        }
        #endif
        
        #endregion

        private void OnApplicationQuit()
        {
        }

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        bool IsUnique(string candidate)
        {
            if(!globalLookup.ContainsKey(candidate))
            {
                return true;
            }
            if(globalLookup[candidate] == this)
            {
                return true;
            }
            if(globalLookup[candidate] == null || (globalLookup[candidate].GetUniqueIdentifier() != candidate))
            {
                globalLookup.Remove(candidate);
                return true;
            }           
            return false;
        }

        /// <summary>
        /// Return the current state of every component on this object that implements the ISaveable interface
        /// </summary>
        /// <returns>an object containing this entity's current state</returns>
        public object CaptureState()
        {
            Dictionary <string, object> stateDict = new Dictionary<string, object>();
            foreach (ISaveable stateHolder in GetComponents<ISaveable>())
            {
                object capturedState = stateHolder.CaptureState();
                stateDict.Add(stateHolder.GetType().ToString(), capturedState);
            }
            return stateDict;
        }

        /// <summary>
        /// Restore this gameobject's state to each of its components that implement the ISaveable interface
        /// </summary>
        /// <param name="state"></param>
        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach(ISaveable stateHolder in GetComponents<ISaveable>())
            {
                string key = stateHolder.GetType().ToString();
                if(stateDict.ContainsKey(key))
                {
                    object savedState = stateDict[key];
                    stateHolder.RestoreState(savedState);
                }
            }                      
        }
    }
}

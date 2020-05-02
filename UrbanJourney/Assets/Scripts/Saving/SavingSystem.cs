using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
       private const string ext = ".sav";
       private const string lastScene = "lastIndex";

        /// <summary>
        /// Write all SaveableEntities in the current scene to the given save file
        /// </summary>
        /// <param name="sceneFile"> the name of the file to write to</param>
        public void Save(string sceneFile)
        {
            Dictionary<string, object> sceneData = LoadFile(sceneFile);
            if(sceneData == null)
            {
                sceneData = new Dictionary<string, object>();
            }
            CaptureState(sceneData);
            SaveFile(sceneFile, sceneData);
        }

        private void SaveFile(string sceneFile, Dictionary<string, object> state)
        {
            string path = GetPathFromSaveFile(sceneFile);
            print("Saving to " + path);

            using (FileStream stream = File.Open(path, FileMode.Create))
            {                
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        // Find every SaveableEntity in the current scene and capture its current state
        private static void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity entity in GameObject.FindObjectsOfType<SaveableEntity>())
            {
                string guid = entity.GetUniqueIdentifier();
                object entityState = entity.CaptureState();
                state[guid] = entityState;

            }
            state[lastScene] = SceneManager.GetActiveScene().buildIndex;
        }

        /// <summary>
        /// Load the given file's save data into the current scene
        /// </summary>
        /// <param name="sceneFile">the file to load</param>
        public void Load (string sceneFile)
		{
           
            Dictionary<string, object> state = LoadFile(sceneFile);
            if(state != null)
            {
                RestoreState(state);
            }
            
		}

        /// <summary>
        /// Delete the given save file 
        /// </summary>
        /// <param name="saveFile">the file to delete</param>
        public void Delete(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            File.Delete(path);
            Debug.Log("Deleted File: " + path);
        }

        /// <summary>
        /// Reload the last loaded scene from the previous savepoint
        /// </summary>
        /// <param name="sceneFile"></param>
        /// <returns></returns>
        public IEnumerator LoadLastScene(string sceneFile)
        {
            Dictionary<string, object> state = LoadFile(sceneFile);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if(state.ContainsKey(lastScene))
            {
                buildIndex = (int) state[lastScene];
                
            }
            
            yield return SceneManager.LoadSceneAsync(buildIndex);            
            RestoreState(state);         
        }

        private Dictionary<string, object> LoadFile(string sceneFile)
        {
            string path = GetPathFromSaveFile(sceneFile);
            print("Loading from " + path);
            
            if (File.Exists(path))
            {
                using (FileStream stream = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (Dictionary<string, object>)formatter.Deserialize(stream);                
                }
            }
            Debug.Log("No save file present");
            return new Dictionary<string, object>();           
        }

        // Find all SaveableEntities in a scene, and pass them their saved state if it exists
        private void RestoreState(Dictionary<string, object> state)
        {
            foreach(SaveableEntity entity in GameObject.FindObjectsOfType<SaveableEntity>())
            {
                string guid = entity.GetUniqueIdentifier();
                if(state.ContainsKey(guid))
                {
                    object entityState = state[guid];
                    entity.RestoreState(entityState);
                }
            }
        }
        
        private string GetPathFromSaveFile (string saveFile)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFile + ext);
            return path;
        }
    }
}

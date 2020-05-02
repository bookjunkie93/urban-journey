using System;
using UnityEngine;

namespace RPG.Saving
{
    /// <summary>
    /// A class used to save state for the nonserializeable Vector3 class
    /// </summary>
    [Serializable]
    public class SerializeableVector3
    {
        float x;
        float y;
        float z;

        public SerializeableVector3(Vector3 vector)
        {
            SerializeVector3(vector);
        }

        public SerializeableVector3()
        {

        }

        public void SerializeVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            Vector3 result = new Vector3(x, y, z);           
            return result;
        }
    }
}

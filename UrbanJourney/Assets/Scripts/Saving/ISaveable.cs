using System;

namespace RPG.Saving
{
    public interface ISaveable
    {
        // The most useful implementation of this method creates a Dictionary<string, object> that contains a key value pair for each
        // piece of state the behaviour wants to serialize
        object CaptureState();

        // Make sure that we treat the param "state" as having the exact same format
        // as the object returned by Capture State!
        void RestoreState(object state);
    }
}

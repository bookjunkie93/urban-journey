using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCommand 
{
    public GrabCommand() {}

    public void Execute(PlayerController agent, GameObject obj)
    {
        agent.Grab(obj);
    }

    public void UnExecute(PlayerController agent)
    {
        agent.Drop();
    }
}

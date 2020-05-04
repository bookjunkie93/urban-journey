using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand
{    
    public MoveCommand(){}

    public void Execute (Mover mover, float x, float z)
    {
        mover.Move(x, false);
    }

   
}

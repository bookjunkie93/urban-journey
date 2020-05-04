using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand
{
    public JumpCommand(){}
    public void Execute (Mover mover)
    {
        mover.Jump();
    }

    public void UnExecute(Mover mover)
    {
        mover.StopJump();
    }
}

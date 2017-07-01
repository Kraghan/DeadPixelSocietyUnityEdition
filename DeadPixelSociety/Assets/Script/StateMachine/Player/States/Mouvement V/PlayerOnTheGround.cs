using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnTheGround : PlayerState
{

    public override void OnEnter()
    {
        player.RearmAnimation();
        player.ResetJumpCounter();
        player.AllowJump();
        //throw new NotImplementedException();
    }

    public override void OnExit()
    {
        //throw new NotImplementedException();
    }

    public override void SFixedUpdate()
    {
        if (player.GetJumpCounter() == 0)
        {
            if (Input.GetKeyDown("space"))
            {
                player.JumpState();
                player.DisallowJump();
            }
        }

        if (player.GetBody().velocity.y < -1.0f)
        {
            player.FallingState();
        }

        //throw new NotImplementedException();
    }

    public override void SStart()
    {
        //throw new NotImplementedException();
    }

    public override void SUpdate()
    {
        //throw new NotImplementedException();
    }
}

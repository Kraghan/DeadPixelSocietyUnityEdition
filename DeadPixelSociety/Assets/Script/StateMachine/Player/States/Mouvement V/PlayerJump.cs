using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerState
{
    public override void OnEnter()
    {
        player.Jump();
        StartAnimation();
        //throw new NotImplementedException();
    }

    public override void OnExit()
    {
        //throw new NotImplementedException();
    }

    public override void SFixedUpdate()
    {
        if (!player.CanJump() && Input.GetKeyUp("space"))
            player.AllowJump();

        if (player.IsOnTheGround())
        {
            player.OnTheGroundState();
        }
        else if (player.CanJump() && Input.GetKeyDown("space"))
        {
            player.DoubleJumpState();
            player.DisallowJump();
        }
        else if(player.GetBody().velocity.y < 0.0f)
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

    public override void StartAnimation()
    {
        if(player.IsAnimated())
            player.GetAnimator().Play("jump_start");
    }
}

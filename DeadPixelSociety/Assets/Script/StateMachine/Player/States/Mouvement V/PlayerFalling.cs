using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : PlayerState
{
    public override void OnEnter()
    {
        StartAnimation();
        //throw new NotImplementedException();
    }

    public override void OnExit()
    {
        //throw new NotImplementedException();
    }

    public override void SFixedUpdate()
    {
        if(Input.GetKeyUp("space"))
        {
            player.AllowJump();
        }

        if (player.IsOnTheGround())
        {
            player.OnTheGroundState();
        }

        if (player.CanJump() && Input.GetKeyDown("space"))
        {
            player.DisallowJump();
            if (player.GetJumpCounter() == 0)
                player.JumpState();
            else if (player.GetJumpCounter() == 1)
                player.DoubleJumpState();
        }
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
            player.GetAnimator().Play("fall_start");
    }
}

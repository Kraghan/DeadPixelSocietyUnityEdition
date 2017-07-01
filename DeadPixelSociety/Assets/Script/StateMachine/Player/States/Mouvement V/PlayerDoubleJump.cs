using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : PlayerState
{
    public override void OnEnter()
    {
        StartAnimation();
        player.Jump();
        //throw new NotImplementedException();
    }

    public override void OnExit()
    {
        //throw new NotImplementedException();
    }

    public override void SFixedUpdate()
    {
        if (player.IsOnTheGround())
        {
            player.OnTheGroundState();
        }
        else if (player.GetBody().velocity.y < 0.0f)
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
        if (player.IsAnimated())
            player.GetAnimator().Play("flip");
    }
}

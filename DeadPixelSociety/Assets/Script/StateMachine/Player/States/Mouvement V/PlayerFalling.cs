using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : PlayerState
{
    private double keyPressedTimer;
    public double timeUntilMaxJumpPower;

    public override void OnEnter()
    {
        keyPressedTimer = 0;
        StartAnimation();
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

        if (Input.GetKeyDown("space"))
        {
            if (player.GetJumpCounter() == 0)
                player.JumpState();
            else if (player.GetJumpCounter() == 1)
                player.DoubleJumpState();
        }
    }

    public override void SStart()
    {
        keyPressedTimer = 0;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : PlayerState
{

    public override void OnEnter()
    {
        //throw new NotImplementedException();
    }

    public override void OnExit()
    {
        //throw new NotImplementedException();
    }

    public override void SFixedUpdate()
    {
        if (Input.GetKey("q"))
        {
            player.MoveToLeft();
            if (Input.GetKey("left shift"))
                player.RunState();
        }
        else if (Input.GetKey("d"))
        {
            player.MoveToRight();
            if (Input.GetKey("left shift"))
                player.RunState();
        }
        else
            player.IdleState();
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
        if (player.IsAnimated() && player.IsOnTheGround())
            player.GetAnimator().Play("walk");
    }
}

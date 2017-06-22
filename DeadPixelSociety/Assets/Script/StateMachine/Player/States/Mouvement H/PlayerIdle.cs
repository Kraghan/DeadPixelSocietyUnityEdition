using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
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
        if (Input.GetKey("q") || Input.GetKey("d"))
        {
            if (Input.GetKey("left shift"))
                player.RunState();
            else
                player.WalkState();
        }
        else
        {
            player.GetBody().velocity = new Vector2(0, player.GetBody().velocity.y);
        }
        //throw new NotImplementedException();
    }

    public override void SStart()
    {
        //sthrow new NotImplementedException();
    }

    public override void SUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void StartAnimation()
    {
        if (player.IsAnimated() && player.IsOnTheGround())
            player.GetAnimator().Play("idle");
    }
}

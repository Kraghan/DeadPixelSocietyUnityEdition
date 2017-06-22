using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerState {

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
        if (Input.GetKey("left shift"))
        {
            if (Input.GetKey("q"))
            {
                player.MoveToLeft(true);
            }
            else if (Input.GetKey("d"))
            {
                player.MoveToRight(true);
            }
        }
        else if (Input.GetKey("q"))
        {
            player.MoveToLeft();
            player.WalkState();
        }
        else if (Input.GetKey("d"))
        {
            player.MoveToRight();
            player.WalkState();
        }
        else
            player.IdleState();

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
            player.GetAnimator().Play("run");
    }
}

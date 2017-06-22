using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnTheGround : PlayerState
{
    private double keyPressedTimer;
    public double timeUntilMaxJumpPower;

    public override void OnEnter()
    {
        keyPressedTimer = 0;
        player.RearmAnimation();
        //throw new NotImplementedException();
    }

    public override void OnExit()
    {
        //throw new NotImplementedException();
    }

    public override void SFixedUpdate()
    {
        if(keyPressedTimer >= timeUntilMaxJumpPower)
        {
            player.JumpState();
            player.Jump(1);
            keyPressedTimer = 0;
        }
        else if (Input.GetKeyUp("space"))
        {
            player.JumpState();
            player.Jump((float)(keyPressedTimer/timeUntilMaxJumpPower));
            keyPressedTimer = 0;
        }
        else if(Input.GetKey("space"))
        {
            keyPressedTimer += Time.fixedDeltaTime;
        }
        else if(player.GetBody().velocity.y < -1.0f)
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

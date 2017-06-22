using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    protected Player player;

    public void SetPlayer(Player player)
    {
        this.player = player;
        for(int i = 0; i < StatesPatern.GetLength(0); ++i)
        {
            PlayerState tmp = (PlayerState)(StatesPatern[i]);
            tmp.SetPlayer(this.player);
        }
    }
}

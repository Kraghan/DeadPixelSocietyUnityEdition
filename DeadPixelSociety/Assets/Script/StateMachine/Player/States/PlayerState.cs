using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State {
    protected Player player;
	
    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public virtual void StartAnimation()
    {

    }
}

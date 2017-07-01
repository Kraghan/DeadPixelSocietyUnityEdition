using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheRight : CommandTriggerDoor
{
    public override void ExecuteEnter(DungeonGenerator dungeon)
    {
        dungeon.ChangeCurrentBlocToRight();
    }

}

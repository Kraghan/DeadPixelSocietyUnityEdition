using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheLeft : CommandTriggerDoor
{

    public override void ExecuteEnter(DungeonGenerator dungeon)
    {
        dungeon.ChangeCurrentBlocToLeft();
    }

}

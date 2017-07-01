using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheTop : CommandTriggerDoor
{
    public override void ExecuteEnter(DungeonGenerator dungeon)
    {
        dungeon.ChangeCurrentBlocToTop();
    }

}

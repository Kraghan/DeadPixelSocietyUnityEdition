using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheBottom : CommandTriggerDoor
{
    public override void ExecuteEnter(DungeonGenerator dungeon)
    {
        dungeon.ChangeCurrentBlocToBottom();
    }
}

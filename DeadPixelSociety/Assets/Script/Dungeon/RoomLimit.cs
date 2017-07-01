using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLimit : MonoBehaviour {

    public DungeonGenerator dungeon;
    private CommandTriggerDoor command;

    private void Start()
    {
        command = GetComponent<CommandTriggerDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger && collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if((!p.IsFrozen() && !p.JustEntered()) || command.force)
            {
                command.ExecuteEnter(dungeon);
                p.SetJustEntered(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if (!p.IsFrozen() && p.JustEntered())
            {
                p.SetJustEntered(false);
            }
        }
    }
}

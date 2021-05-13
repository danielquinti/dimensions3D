using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.AI;

public class TrapRoomManager : RoomManager
{
    private bool active = false;

    public override void OnEnterRoom()
    {
        // open a door in an activated room
        if (active)
        {
            enemyManager.RestoreSpawnPoints();
        }
        // enter a disabled trap room
        else
        {
            foreach (VendingDoor door in doors)
            {
                door.Open();
            }
        }
    }

    public void ActivateTrap()
    {
        active = true;
        // only the spawners in this room can be active while the player remains trapped
        enemyManager.ChangeSpawnPoints(spawners);
        // close every door
        foreach (VendingDoor door in doors)
        {
            door.Close();
        }
    }

    public void DeactivateTrap()
    {
        foreach (VendingDoor door in doors)
        {
            door.Open();
        }
        enemyManager.RestoreSpawnPoints();
    }
}

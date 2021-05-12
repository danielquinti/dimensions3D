using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.AI;
public class TrapRoomManager : RoomManager
{
    private bool active = false;
    public override void OnEnterRoom()
    {
        if (active){
            enemyManager.RestoreSpawnPoints();
        }
        else
        {
            foreach (VendingDoor door in doors)
            {
                door.Disable();
            }
        }
    }

    public void ActivateTrap()
    {
        active = true;
        enemyManager.ChangeSpawnPoints(spawners);
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
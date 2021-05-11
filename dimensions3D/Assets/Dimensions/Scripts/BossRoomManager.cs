using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.AI;
public class BossRoomManager : RoomManager
{
    public override void OnOpenRoom()
    {
        foreach (VendingDoor door in doors)
        {
            door.Disable();
        }
    }
}

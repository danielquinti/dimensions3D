using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.AI;
public class RoomManager : MonoBehaviour
{
    public List<Transform> spawners;
    public List<VendingDoor> doors;
    public Unity.FPS.AI.CustomEnemyManager enemyManager;
    
    public void OnOpenRoom()
    {
        enemyManager.AddSpawnPoints(spawners);
        foreach (VendingDoor door in doors){
            door.Disable();
        }
        Destroy(this);
    }
}

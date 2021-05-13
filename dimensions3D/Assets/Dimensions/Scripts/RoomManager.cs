using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.AI;
public class RoomManager : MonoBehaviour
{
    public List<Transform> spawners;
    public List<VendingDoor> doors;
    protected Unity.FPS.AI.CustomEnemyManager enemyManager;

    void Start()
    {
        enemyManager = GameObject.Find("CustomGameManager").GetComponent<CustomEnemyManager>();
    }

    // when any of the doors to a room is opened
    public virtual void OnEnterRoom()
    {
        // enemies start spawning in the room
        enemyManager.AddSpawnPoints(spawners);
        // every door leading to the same door is opened
        foreach (VendingDoor door in doors)
        {
            door.Open();
        }
        Destroy(this);
    }
}

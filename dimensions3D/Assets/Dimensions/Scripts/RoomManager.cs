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

    public virtual void OnEnterRoom()
    {
        enemyManager.AddSpawnPoints(spawners);
        foreach (VendingDoor door in doors){
            door.Disable();
        }
        Destroy(this);
    }
}

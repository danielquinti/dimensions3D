using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.AI;
public class SpawnerManager : MonoBehaviour
{
    public List<Transform> spawners;
    public VendingDoor door;
    public Unity.FPS.AI.CustomEnemyManager manager;

    // Update is called once per frame
    void Update()
    {
        if (!door.IsActive())
        {
            manager.AddSpawnPoints(spawners);
            Destroy(this);
        }
    }
}

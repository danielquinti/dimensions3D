using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.FPS.Game;
public class EndTrigger : MonoBehaviour
{
    virtual public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Unity.FPS.Game.EventManager.Broadcast(Unity.FPS.Game.Events.AllObjectivesCompletedEvent);
            Destroy(this);
        }
    }
}

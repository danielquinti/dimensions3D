using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class VendingDoor : Vending
{
    public Animator doorAnimator;
    public RoomManager manager;
    override public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            display.SetInfo("Press F to open for " + price + " tokens.");
            other.GetComponent<Wallet>().SetReachable(this);
        }
    }
    
    override public void GetReward(MonoBehaviour player)
    {
        display.SetInfo("Door opened.");
        player.GetComponent<Wallet>().SetReachable(null);
        manager.OnOpenRoom();
        Disable();
    }

    public void Disable()
    {
        doorAnimator.SetBool("character_nearby", true);
        Destroy(this);
    }
}

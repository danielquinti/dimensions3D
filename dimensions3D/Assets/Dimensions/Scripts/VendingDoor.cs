using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class VendingDoor : Vending
{
    public Animator doorAnimator;
    
    override public void OnTriggerEnter (Collider other)
    {
        if ((other.gameObject.tag == "Player") & active)
        {
            display.SetInfo("Press F to open for " + price + " tokens.");
            other.GetComponent<Wallet>().SetReachable(this);
        }
    }
    
    override public void GetReward(MonoBehaviour player)
    {
        display.SetInfo("Door opened.");
    	// set animator to open
    	doorAnimator.SetBool("character_nearby", true);
        active = false;
        player.GetComponent<Wallet>().SetReachable(null);
    }

    public bool IsActive()
    {
        return active;
    }
}

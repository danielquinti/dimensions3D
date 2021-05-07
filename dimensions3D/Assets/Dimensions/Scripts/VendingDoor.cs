using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class VendingDoor : Vending
{
    public Animator doorAnimator;
    public RoomManager manager;

    override public void ShowPrice()
    {
        display.SetInfo("Press F to open for " + price + " tokens.");
    }
    override public void Sell(MonoBehaviour player)
    {
        display.SetInfo("Door opened.");
        base.Sell(player);
        manager.OnOpenRoom();
        Disable();
    }

    public void Disable()
    {
        doorAnimator.SetBool("character_nearby", true);
        Destroy(this);
    }
}

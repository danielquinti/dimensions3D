using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class VendingDoor : Vending
{
    public Animator doorAnimator;
    public RoomManager manager;
    private bool active = true;

    override public void ShowPrice()
    {
        if (active) { display.SetInfo("Press F to open for " + price + " tokens."); }
    }
    override public void Sell(MonoBehaviour player)
    {
        display.SetInfo("Door opened.");
        base.Sell(player);
        manager.OnOpenRoom();
        Disable();
    }

    public void Open()
    {
        doorAnimator.SetBool("character_nearby", true);
    }

    public void Disable()
    {
        Open();
        active = false;
    }

    public void Close()
    {
        doorAnimator.SetBool("character_nearby", false);
        active = true;
    }
}

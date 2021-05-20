using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.Events;

public class VendingDoor : Vending
{
    public Animator doorAnimator;
    public RoomManager manager;
    private bool active = true;
    public bool useCallBack;
    public UnityEvent saleEvent;
    
    override public void ShowPrice()
    {
        // only show prompt if the door is closed
        if (active) { display.SetInfo("Press F to open for " + price + " tokens."); }
    }

    override public void Sell(Wallet wallet)
    {
        base.Sell(wallet);
        Open();
        if (!useCallBack)
            // default behaviour
            manager.OnEnterRoom();
        else
            // can be added by inspector
            saleEvent?.Invoke();
    }

    public void Open()
    {
        doorAnimator.SetBool("character_nearby", true);
        active = false;
    }

    public void Close()
    {
        doorAnimator.SetBool("character_nearby", false);
        active = true;
    }
}

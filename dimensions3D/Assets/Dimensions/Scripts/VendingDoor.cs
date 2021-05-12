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
        if (active) { display.SetInfo("Press F to open for " + price + " tokens."); }
    }

    override public void Sell(MonoBehaviour player)
    {
        display.SetInfo("Door opened.");
        base.Sell(player);
        if (!useCallBack)
            manager.OnEnterRoom();
        else
            saleEvent?.Invoke();
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

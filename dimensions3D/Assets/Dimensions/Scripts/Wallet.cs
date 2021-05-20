using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wallet : MonoBehaviour
{
    [Tooltip("Starting balance")] public int StartingBalance = 10;

    Unity.FPS.Gameplay.PlayerInputHandler m_InputHandler;

    protected int CurrentBalance;
    // reference of the vending station with which the player can interact
    protected Vending reachable;
    protected InfoDisplay display;

    void Start()
    {
        // hardcoded reference to avoid inspector-heavy handling
        display = GameObject.Find("WalletBalance").GetComponent<InfoDisplay>();
        CurrentBalance = StartingBalance;
        m_InputHandler = GetComponent<Unity.FPS.Gameplay.PlayerInputHandler>();
        display.SetInfo(CurrentBalance.ToString());
    }

    public void Earn(int amount)
    {
        CurrentBalance += amount;
        // update display
        display.SetInfo(CurrentBalance.ToString());
    }

    public bool Pay(int amount)
    {
        // if the price is not too high
        if (CurrentBalance - amount >= 0)
        {
            // pay
            CurrentBalance -= amount;
            display.SetInfo(CurrentBalance.ToString());
            return true;
        }
        else return false;
    }

    protected void TryPay()
    {
        if (this.reachable != null && Pay(this.reachable.price))
        {
            // generic sell function from the reachable vending
            this.reachable.Sell(this);
        }
        // if price is too high
        else if (this.reachable != null)
        {
            this.reachable.Decline();
        }
    }

    void Update()
    {
        // Check if f pressed
        if (m_InputHandler.GetInteractButtonDown())
        {
            TryPay();
        }
    }

    public void SetReachable(Vending reach)
    {
        this.reachable = reach;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wallet : MonoBehaviour
{
    [Tooltip("Starting balance")] public int StartingBalance = 10;
    
    Unity.FPS.Gameplay.PlayerInputHandler m_InputHandler;

    protected int CurrentBalance;
    protected Vending reachable;
    protected InfoDisplay display;

    void Start()
    {
        display = GameObject.Find("WalletBalance").GetComponent<InfoDisplay>();
        CurrentBalance = StartingBalance;
        m_InputHandler = GetComponent<Unity.FPS.Gameplay.PlayerInputHandler>();
        display.SetInfo(CurrentBalance.ToString());
    }

    public void Earn(int amount)
    {
        float balanceBefore = CurrentBalance;
        CurrentBalance += amount;
        display.SetInfo(CurrentBalance.ToString());
    }

    public bool Pay(int amount)
    {
        float BalanceBefore = CurrentBalance;
        if (CurrentBalance - amount >= 0)
        {
            CurrentBalance -= amount;
            display.SetInfo(CurrentBalance.ToString());
            return true;
        }
        else return false;
    }
    
    public void TryPay()
    {
        if (this.reachable != null && Pay(this.reachable.price))
        {
            this.reachable.Sell(this);
        }
        else if (this.reachable != null)
        {
            this.reachable.Decline();
        }
    }
    
    void Update()
    {
        if (m_InputHandler.GetInteractButtonDown())
        {
            // Check if f pressed
            TryPay();
        }
    }
    
    public void SetReachable(Vending reach)
    {
        this.reachable = reach;
    }
}

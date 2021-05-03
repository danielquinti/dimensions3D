using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wallet : MonoBehaviour
{
    [Tooltip("Starting balance")] public int StartingBalance = 10;
    
    Unity.FPS.Gameplay.PlayerInputHandler m_InputHandler;

    public int CurrentBalance;
    public Vending reachable;
    public WalletBalanceDisplay display;

    void Start()
    {
        CurrentBalance = StartingBalance;
        m_InputHandler = GetComponent<Unity.FPS.Gameplay.PlayerInputHandler>();
        display.ChangeBalance(CurrentBalance);
    }

    public void Earn(int amount)
    {
        float balanceBefore = CurrentBalance;
        CurrentBalance += amount;
        display.ChangeBalance(CurrentBalance);
    }

    public bool Pay(int amount)
    {
        float BalanceBefore = CurrentBalance;
        if (CurrentBalance - amount >= 0)
        {
            CurrentBalance -= amount;
            display.ChangeBalance(CurrentBalance);
            return true;
        }
        else return false;
    }
    
    public void TryPay()
    {
        if (this.reachable != null && Pay(this.reachable.price))
        {
            this.reachable.GetReward(this);
        }
        else
        {
            Debug.Log("Tried");
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

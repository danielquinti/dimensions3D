using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [Tooltip("Starting balance")] public int StartingBalance = 10;
    
    Unity.FPS.Gameplay.PlayerInputHandler m_InputHandler;

    public float CurrentBalance;
    public Vending reachable;

    void Start()
    {
        CurrentBalance = StartingBalance;
        m_InputHandler = GetComponent<Unity.FPS.Gameplay.PlayerInputHandler>();
    }

    public void Earn(float amount)
    {
        float balanceBefore = CurrentBalance;
        CurrentBalance += amount;
        Debug.Log("Current Balance:" + CurrentBalance.ToString());
    }

    public bool Pay(float amount)
    {
        float BalanceBefore = CurrentBalance;
        if (CurrentBalance - amount >= 0)
        {
            CurrentBalance -= amount;
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

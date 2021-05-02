using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [Tooltip("Starting balance")] public int StartingBalance = 10;
    
    

    public float CurrentBalance;
    public Vending reachable;

    void Start()
    {
        CurrentBalance = StartingBalance;
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
        if (this.reachable == null && Pay(this.reachable.price))
        {
            this.reachable.GetReward(this);
        }
    }
    
    public void SetReachable(Vending reach)
    {
        this.reachable = reach;
    }
}

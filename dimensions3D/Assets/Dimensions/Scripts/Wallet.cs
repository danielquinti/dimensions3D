using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [Tooltip("Starting balance")] public int StartingBalance = 10;

    public float CurrentBalance;

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
}
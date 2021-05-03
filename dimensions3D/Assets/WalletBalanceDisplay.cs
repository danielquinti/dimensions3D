using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WalletBalanceDisplay : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI display;
    int balance;
    public void ChangeBalance(int actual)
    {
        balance = actual;
    }
    // Update is called once per frame
    void Update()
    {
        display.text = balance.ToString();
    }
}

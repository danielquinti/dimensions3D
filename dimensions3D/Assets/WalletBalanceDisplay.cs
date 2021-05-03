using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WalletBalanceDisplay : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI display;
    // Update is called once per frame
    void Update()
    {
        display.text = player.GetComponent<Wallet>().CurrentBalance.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.Game;

public class Vending : MonoBehaviour
{
    public int price = 500;
    public InfoDisplay display;
    public AudioClip Sold;
    public AudioClip Declined;
    
    virtual public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowPrice();
            other.GetComponent<Wallet>().SetReachable(this);
        }
    }

    virtual public void ShowPrice()
    {
        return;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            display.SetInfo("");
            other.GetComponent<Wallet>().SetReachable(null);
        }
    }
    
    virtual public void Sell(MonoBehaviour player)
    {
        AudioUtility.CreateSFX(Sold, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        player.GetComponent<Wallet>().SetReachable(null);
    }

    public void Decline()
    {
        AudioUtility.CreateSFX(Declined, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        display.SetInfo("Not enough tokens.");
    }
}

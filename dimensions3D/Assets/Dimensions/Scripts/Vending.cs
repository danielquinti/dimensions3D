using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.Game;

public abstract class Vending : MonoBehaviour
{
    public int price = 500;
    protected InfoDisplay display;
    public AudioClip Sold;
    public AudioClip Declined;

    virtual public void Start()
    {
        // hardcode display reference to avoid inspector-heavy handling of vending stations
        display = GameObject.Find("InteractDisplay").GetComponent<InfoDisplay>();
    }

    virtual public void OnTriggerEnter(Collider other)
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
            // reset display text
            display.SetInfo("");
            // reset reachable station
            other.GetComponent<Wallet>().SetReachable(null);
        }
    }

    virtual public void Sell(MonoBehaviour player)
    {
        // play sell sound
        AudioUtility.CreateSFX(Sold, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        player.GetComponent<Wallet>().SetReachable(null);
    }

    public void Decline()
    {
        // play decline sound
        AudioUtility.CreateSFX(Declined, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        display.SetInfo("Not enough tokens.");
    }
}

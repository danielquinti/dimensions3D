using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.Game;

public class Vending : MonoBehaviour
{
    public int price = 500;
    public string itemName = "";
    public InfoDisplay display;
    public AudioClip Sold;
    public AudioClip Declined;
    public Unity.FPS.Game.WeaponController weapon;
    protected bool active = true;
    
    virtual public void OnTriggerEnter (Collider other)
    {
        if ((other.gameObject.tag == "Player") & active)
        {
            display.SetInfo("Press F to buy " + itemName + " for " + price + " tokens.");
            other.GetComponent<Wallet>().SetReachable(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            display.SetInfo("");
            other.GetComponent<Wallet>().SetReachable(null);
        }
    }
    
    virtual public void GetReward(MonoBehaviour player)
    {
        AudioUtility.CreateSFX(Sold, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        display.SetInfo(itemName + " acquired.");
    	player.GetComponent<PlayerWeaponsManager>().AddWeapon(weapon);
        active = false;
        player.GetComponent<Wallet>().SetReachable(null);
    }

    public void Decline()
    {
        AudioUtility.CreateSFX(Declined, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        display.SetInfo("Not enough tokens.");
    }
}

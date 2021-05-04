using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class Vending : MonoBehaviour
{
    public int price = 500;
    public string itemName = "";
    public InfoDisplay display;
    public Unity.FPS.Game.WeaponController weapon;
    bool active = true;
    
    void OnTriggerEnter (Collider other)
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
    
    public void GetReward(MonoBehaviour player)
    {
        display.SetInfo(itemName + " acquired.");
    	player.GetComponent<PlayerWeaponsManager>().AddWeapon(weapon);
        active = false;
        player.GetComponent<Wallet>().SetReachable(null);
    }

    public void Decline()
    {
        display.SetInfo("Not enough tokens.");
    }
}

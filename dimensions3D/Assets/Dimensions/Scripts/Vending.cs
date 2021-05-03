using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class Vending : MonoBehaviour
{
    public int price = 500;
    public Unity.FPS.Game.WeaponController weapon;
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Object Entered the trigger");
            other.GetComponent<Wallet>().SetReachable(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Object exited the trigger");
            other.GetComponent<Wallet>().SetReachable(null);
        }
    }
    
    public void GetReward(MonoBehaviour wallet)
    {
    	wallet.GetComponent<PlayerWeaponsManager>().AddWeapon(weapon);
    }
}

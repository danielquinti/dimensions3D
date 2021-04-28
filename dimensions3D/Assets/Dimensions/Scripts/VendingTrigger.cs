using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class VendingTrigger : MonoBehaviour
{
    public int price = 500;
    public Unity.FPS.Game.WeaponController weapon;
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Object Entered the trigger");
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<Wallet>().Pay(price)){
                other.GetComponent<PlayerWeaponsManager>().AddWeapon(weapon);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Object exited the trigger");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.Game;

public class VendingWeapon : Vending
{
    public Unity.FPS.Game.WeaponController weapon;
    public string itemName = "";

    override public void ShowPrice()
    {
        display.SetInfo("Press F to buy " + itemName + " for " + price + " tokens.");
    }

    override public void Sell(MonoBehaviour player)
    {
        // play sound
        AudioUtility.CreateSFX(Sold, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        display.SetInfo(itemName + " acquired.");
        // acquire weapon
        player.GetComponent<PlayerWeaponsManager>().AddWeapon(weapon);
        // reset player-reachable vending stations
        player.GetComponent<Wallet>().SetReachable(null);
        Destroy(this);
    }
}

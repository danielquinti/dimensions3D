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

    override public void Sell(Wallet wallet)
    {
        base.Sell(wallet);
        // acquire weapon
        wallet.GetComponent<PlayerWeaponsManager>().AddWeapon(weapon);
        Destroy(this);
    }
}
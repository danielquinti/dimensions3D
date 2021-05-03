using Unity.FPS.Gameplay;
using UnityEngine;

public class GearPickup : Unity.FPS.Gameplay.Pickup
{
    [Header("Parameters")] [Tooltip("Loot reward")]
    public int Value;

    protected override void OnPicked(PlayerCharacterController player)
    {
        Wallet playerWallet = player.GetComponent<Wallet>();
        playerWallet.Earn(Value);
        PlayPickupFeedback();
        Destroy(gameObject);
    }
}
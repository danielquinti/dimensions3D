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
        // play the sound and/or vfx stored in the script as serialized attributes
        PlayPickupFeedback();
        Destroy(gameObject);
    }
}
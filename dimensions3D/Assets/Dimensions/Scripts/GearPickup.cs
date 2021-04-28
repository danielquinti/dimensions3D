using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class GearPickup : Pickup
    {
        [Header("Parameters")] [Tooltip("Loot reward")]
        public float Value;

        protected override void OnPicked(PlayerCharacterController player)
        {
            Wallet playerWallet = player.GetComponent<Wallet>();
            playerWallet.Earn(Value);
            PlayPickupFeedback();
            Destroy(gameObject);
        }
    }
}
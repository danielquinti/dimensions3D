using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class MoneyPickup : Pickup
    {

        protected override void OnPicked(PlayerCharacterController player)
        {
            PlayPickupFeedback();
            Destroy(gameObject);
        }
    }
}
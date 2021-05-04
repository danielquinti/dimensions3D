using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Unity.FPS.AI
{
    [RequireComponent(typeof(Health), typeof(Actor), typeof(NavMeshAgent))]
    public class MeleeController : CustomEnemyController
    {
        public override bool TryAtack(Vector3 enemyPosition)
        {
            return true;
        }
    }
}
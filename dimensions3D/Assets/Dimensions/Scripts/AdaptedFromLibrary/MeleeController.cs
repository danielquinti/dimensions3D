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
        // attack from the base class is supressed
        public override bool TryAtack(Vector3 enemyPosition)
        {
            return true;
        }
    }
}
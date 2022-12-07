using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.UtilityAI;
using TL.Core;

namespace TL.UtilityAI.Actions
{

    [CreateAssetMenu(fileName = "Work", menuName = "UtilityAI/Actions/Work")]
    public class Work : Action
    {
        public override void Execute(NPCController npc)
        {
            npc.DoWork(3);
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            npc.context.RefreshDestinations();

            float distance = Mathf.Infinity;
            Transform nearestCoffeePlant = null;

            List<Transform> coffeePlants = npc.context.Destinations[DestinationType.coffeePlant];
            foreach(Transform coffeePlant in coffeePlants)
            {
                float distanceFromResource = Vector3.Distance(coffeePlant.position, npc.transform.position);
                if (distanceFromResource < distance)
                {
                    nearestCoffeePlant = coffeePlant;
                    distance = distanceFromResource;
                }
            }

            RequiredDestination = nearestCoffeePlant;
            npc.mover.destination = RequiredDestination;
        }

    }
}

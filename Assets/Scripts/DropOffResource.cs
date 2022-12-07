using System.Collections;
using System.Collections.Generic;
using TL.Core;
using UnityEngine;

namespace TL.UtilityAI.Actions
{

    [CreateAssetMenu(fileName = "DropOffResource", menuName = "UtilityAI/Actions/DropOffResource")]

    public class DropOffResource : Action
    {
        public override void Execute(NPCController npc)
        {
            Debug.Log("Dropped Off Item");
            npc.inventory.RemoveAllResource();
            npc.stats.money += 20;
            npc.aiBrain.finishedExecutingBestAction = true;
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            npc.context.RefreshDestinations();

            float distance = Mathf.Infinity;
            Transform nearestStorageDestination = null;

            List<Transform> storageDestinations = npc.context.Destinations[DestinationType.storage];
            foreach (Transform storageDestination in storageDestinations)
            {
                float distanceFromStorage = Vector3.Distance(storageDestination.position, npc.transform.position);
                if (distanceFromStorage < distance)
                {
                    nearestStorageDestination = storageDestination;
                    distance = distanceFromStorage;
                }
            }

            RequiredDestination = nearestStorageDestination;
            npc.mover.destination = RequiredDestination;
        }
    }
}

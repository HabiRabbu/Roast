using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using TL.UtilityAI;

namespace TL.UtilityAI.Actions
{

    [CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
    public class Sleep : Action
    {
        public override void Execute(NPCController npc)
        {
            if (npc.context.Destinations[DestinationType.sleep] != null)
            {
                npc.DoSleep(3);
            }
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            //npc.context.RefreshDestinations();

            float distance = Mathf.Infinity;
            Transform nearestSleepDestination = null;

            if (npc.context.Destinations[DestinationType.sleep] != null)
            {
                List<Transform> sleepDestinations = npc.context.Destinations[DestinationType.sleep];
                foreach (Transform sleepDestination in sleepDestinations)
                {
                    float distanceFromSleep = Vector3.Distance(sleepDestination.position, npc.transform.position);
                    if (distanceFromSleep < distance)
                    {
                        nearestSleepDestination = sleepDestination;
                        distance = distanceFromSleep;
                    }
                }

                RequiredDestination = nearestSleepDestination;
                npc.mover.destination = RequiredDestination;
            }
            else
            {
                npc.mover.destination = npc.transform;
            }
            
        }
    }
}

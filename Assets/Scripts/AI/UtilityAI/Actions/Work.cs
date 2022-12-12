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
            //if work is available, do work
            if (RequiredDestination != null)
            {
                npc.DoWork();
            }
            //else
            else
            {
                npc.DoIdle();
            }
                //npc.DoIdle(5)
        }

        public override void SetRequiredDestination(NPCController npc)
        {

            //if work is available find a destination
            if (npc.context.Destinations[DestinationType.coffeePlant] != null)
            {

                float distance = Mathf.Infinity;
                Transform nearestCoffeePlant = null;

                List<Transform> coffeePlants = npc.context.Destinations[DestinationType.coffeePlant];
                foreach (Transform coffeePlant in coffeePlants)
                {
                    if (coffeePlant.gameObject.GetComponent<CoffeePlant>().isWorkAvailable)
                    {

                        float distanceFromResource = Vector3.Distance(coffeePlant.position, npc.transform.position);
                        if (distanceFromResource < distance)
                        {
                            nearestCoffeePlant = coffeePlant;
                            distance = distanceFromResource;
                        }
                    }
                }
                if (nearestCoffeePlant != null)
                {
                    RequiredDestination = nearestCoffeePlant;
                    npc.mover.destination = RequiredDestination;
                }
                else
                {
                    RequiredDestination = null;
                    npc.mover.destination = npc.transform;
                }
            }
            else
            {
                RequiredDestination = null;
                npc.mover.destination = npc.transform;
            }
        }

    }
}

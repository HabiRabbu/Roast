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
            if (npc.context.Destinations.ContainsKey(DestinationType.workObject))
            {
                if (npc.context.Destinations[DestinationType.workObject].Count > 0)
                {
                    float distance = Mathf.Infinity;
                    Transform nearestWorkObject = null;

                    List<Transform> workObjects = npc.context.Destinations[DestinationType.workObject];
                    foreach (Transform workObject in workObjects)
                    {
                        if (workObject.gameObject.GetComponent<WorkObject>().isWorkAvailable)
                        {

                            float distanceFromResource = Vector3.Distance(workObject.position, npc.transform.position);
                            if (distanceFromResource < distance)
                            {
                                nearestWorkObject = workObject;
                                distance = distanceFromResource;
                            }
                        }
                    }
                    if (nearestWorkObject != null)
                    {
                        RequiredDestination = nearestWorkObject;
                        npc.mover.destination = RequiredDestination;
                    }
                    else
                    {
                        RequiredDestination = null;
                        npc.mover.destination = npc.transform;
                    }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using TL.UtilityAI;

namespace TL.UtilityAI.Actions
{

    [CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
    public class Eat : Action
    {
        public override void Execute(NPCController npc)
        {
            Debug.Log("I ate something!");
            //Logic for eating food
            npc.stats.hunger -= 30;
            npc.stats.money -= 10;


            //Decide new action after this action
            //npc.OnFinishedAction();
            npc.aiBrain.finishedExecutingBestAction = true;
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            RequiredDestination = npc.transform;
        }
    }
}

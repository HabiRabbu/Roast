using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using TL.UtilityAI;

namespace TL.UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Idle", menuName = "UtilityAI/Actions/Idle")]
    public class Idle : Action
    {
        public override void Execute(NPCController npc)
        {
            npc.DoIdle();
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            RequiredDestination = null;
            //Random for wander, or in coroutine(?)
        }
    }
}

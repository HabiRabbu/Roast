using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using TL.UtilityAI;


namespace TL.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "IsWorkAvailableConsideration", menuName = "UtilityAI/Considerations/Is Work Available Consideration")]
    public class IsWorkAvailableConsideration : Consideration
    {
        public override float ScoreConsideration(NPCController npc)
        {
            if (GameObject.Find("ActionManager").GetComponent<ActionManager>().isWorkAvailable)
            {
                return 0f;
            }
            else
            {
                return Mathf.Clamp01(0.3f);
            }
        }
    }
}
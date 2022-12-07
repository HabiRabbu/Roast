using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using TL.UtilityAI;


namespace TL.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "HowFullIsMyInventory", menuName = "UtilityAI/Considerations/HowFullIsMyInventory")]
    public class HowFullIsMyInventory : Consideration
    {

        [SerializeField] private AnimationCurve responseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            score = responseCurve.Evaluate(Mathf.Clamp01(npc.inventory.HowFullIsStorage()));
            return score;
        }
    }
}

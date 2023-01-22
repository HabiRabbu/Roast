using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using TL.UtilityAI;


namespace TL.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "WorkNeededConsideration", menuName = "UtilityAI/Considerations/Work Needed Consideration")]

    public class WorkNeededConsideration : Consideration
    {
        [SerializeField] private AnimationCurve responseCurve;
        public override float ScoreConsideration(NPCController npc)
        {
            //score = workNeeded/workPossible
            float workNeeded = (float) GameObject.Find("ActionManager").GetComponent<ActionManager>().workTasksAvailable;
            float workPossible = (float) GameObject.Find("Context").GetComponent<Context>().totalWorkPossible;
            score = responseCurve.Evaluate(Mathf.Clamp01(workNeeded / workPossible));
            return score;
        }
    }
}

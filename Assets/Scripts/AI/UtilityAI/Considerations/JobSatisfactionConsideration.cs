using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using TL.UtilityAI;


namespace TL.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "JobSatisfactionConsideration", menuName = "UtilityAI/Considerations/Job Satisfaction Consideration")]

    public class JobSatisfactionConsideration : Consideration
    {
        [SerializeField] private AnimationCurve responseCurve;
        public override float ScoreConsideration(NPCController npc)
        {
            //score = jobSatisfaction/maxJobSatisfaction
            score = responseCurve.Evaluate(Mathf.Clamp01(npc.stats.jobSatisfaction / npc.stats.maxJobSatisfaction));
            return score;
        }
    }
}

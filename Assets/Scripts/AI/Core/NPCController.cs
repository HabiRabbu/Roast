using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.UtilityAI;

namespace TL.Core
{

    public enum State
    {
        decide,
        move,
        execute
    }

    public class NPCController : MonoBehaviour
    {
        public ActionManager actionManager;
        public MoveController mover { get; set; }
        public AIBrain aiBrain { get; set; }
        public NPCInventory inventory { get; set; }
        public StaffStats stats { get; set; }

        public Context context;

        public CoffeePlant coffeePlant;

        public float minDistanceFromTarget;
        public float timePerOneWorkInSeconds;

        public Coroutine working;
        public Coroutine sleeping;
        public Coroutine idling;


        public State currentState { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<MoveController>();
            aiBrain = GetComponent<AIBrain>();
            inventory = GetComponent<NPCInventory>();
            stats = GetComponent<StaffStats>();

            working = null;
            sleeping = null;
            idling = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (aiBrain.finishedDeciding)
            {
                aiBrain.finishedDeciding = false;
                aiBrain.bestAction.Execute(this);
            }

            stats.UpdateEnergy(AmIAtRestDestination());
            stats.UpdateHunger();

            FSMTick();
        }

        public void FSMTick()
        {
            if (currentState == State.decide)
            {
                    aiBrain.DecideBestAction();

                    if (aiBrain.bestAction.RequiredDestination != null)
                    {
                        if (Vector3.Distance(aiBrain.bestAction.RequiredDestination.position, this.transform.position) < minDistanceFromTarget)
                        {
                            currentState = State.execute;
                        }
                        else
                        {
                            currentState = State.move;
                        }
                    }
            }
            else if (currentState == State.move)
            {
                if (Vector3.Distance(aiBrain.bestAction.RequiredDestination.position, this.transform.position) < minDistanceFromTarget)
                {
                    currentState = State.execute;
                }
                else
                {
                    mover.MoveTo(aiBrain.bestAction.RequiredDestination.position);
                }
            }
            else if (currentState == State.execute)
            {
                if (aiBrain.finishedExecutingBestAction == false)
                {
                    aiBrain.bestAction.Execute(this);
                }
                else if (aiBrain.finishedExecutingBestAction == true)
                {
                    currentState = State.decide;
                }
            }
        }

        #region WorkHorse
        public void OnFinishedAction()
        {
            context.RefreshDestinations();
            aiBrain.DecideBestAction();
        }
        public bool AmIAtRestDestination()
        {
            if(context.GetClosestSleepDestination(this) != null)
            {
                return Vector3.Distance(this.transform.position, context.GetClosestSleepDestination(this).position) <= context.MinDistance;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region Coroutine

        public void DoWork()
        {
            if (working == null)
            {
                working = StartCoroutine(WorkCoroutine(timePerOneWorkInSeconds));
            }
        }

        public void DoSleep(int time)
        {
            if (sleeping == null)
            {
                sleeping = StartCoroutine(SleepCoroutine(time));
            }
        }

        public void DoIdle()
        {
            if (idling == null)
            {
                idling = StartCoroutine(IdleCoroutine());
            }
        }

        IEnumerator IdleCoroutine()
        {
            Debug.Log("I have nothing to do.");

            yield return new WaitForSeconds(5);

            aiBrain.finishedExecutingBestAction = true;
            idling = null;
        }

        IEnumerator WorkCoroutine(float time)
        {
                
            yield return new WaitForSeconds(time);
 

            //TODO: Add logic for farming/working etc
            aiBrain.bestAction.RequiredDestination.gameObject.GetComponent<CoffeePlant>().WorkAction(this);

            //Decide new action after this action
            aiBrain.finishedExecutingBestAction = true;
            working = null;
        }

        IEnumerator SleepCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }

            Debug.Log("I just slept for 1 energy point.");
            //TODO: Add Logic for adding energy to the NPC.
            stats.energy += 1;

            //Decide new action after this action
            //OnFinishedAction();
            aiBrain.finishedExecutingBestAction = true;
            sleeping = null;
        }

        #endregion
    }
}

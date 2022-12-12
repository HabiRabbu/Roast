using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;

namespace TL.UtilityAI
{

    public class ActionManager : MonoBehaviour
    {
        public WorldTimeManager worldTimeManager;
        public List<Action> actionsAvailable;
        public Context context;

        public Action work;
        public Action sleep;
        public Action dropOff;

        public int workTasksAvailable;

        void Start()
        {
            context = GameObject.Find("Context").GetComponent<Context>();
            workTasksAvailable = 0;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void FindAvailableActions()
        {
            AddRemoveWork();
            AddRemoveSleep();

            //Something else
        }

        private void AddRemoveWork()
        {
            //WORK
            //Add work action if workTasksAvailable
            if (workTasksAvailable > 0)
            {
                bool workAdded = false;
                foreach (Action action in actionsAvailable)
                {
                    if (action.Name == work.Name)
                    {
                        workAdded = true;
                    }
                }
                if (workAdded == false)
                {
                    actionsAvailable.Add(work);
                    Debug.Log("Work action added!");
                }
            }
            //Remove work action if !workTasksAvailable
            else
            {
                foreach (Action action in actionsAvailable)
                {
                    if (action.Name == work.Name)
                    {
                        actionsAvailable.Remove(action);
                        Debug.Log("Work action removed!");
                    }
                }
            }
        }

        private void AddRemoveSleep()
        {
            //SLEEP
            //Add if sleep locations is available
            if (context.Destinations.ContainsKey(DestinationType.sleep))
            {
                if(context.Destinations[DestinationType.sleep].Count > 0)
                {
                    bool sleepAdded = false;
                    foreach (Action action in actionsAvailable)
                    {
                        if (action.Name == sleep.Name)
                        {
                            sleepAdded = true;
                        }
                    }
                    if (sleepAdded == false)
                    {
                        actionsAvailable.Add(sleep);
                        Debug.Log("Sleep action added!");
                    }
                }
                else
                {
                    foreach (Action action in actionsAvailable)
                    {
                        if (action.Name == sleep.Name)
                        {
                            actionsAvailable.Remove(action);
                            Debug.Log("Sleep action removed!");
                        }
                    }
                }
            }

            //Remove if no sleep locations are available
        }

        public void AddWorkTask(int amount)
        {
            workTasksAvailable += amount;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;

namespace TL.UtilityAI
{

    public class ActionManager : MonoBehaviour
    {
        public AIBrain aiBrain;
        public WorldTimeManager worldTimeManager;

        public Action work;
        public Action sleep;
        public Action dropOff;

        public Context context;

        public bool isWorkAvailable { get; set; }
        public int workTasksAvailable;

        private bool workAdded;
        void Start()
        {
            context = GameObject.Find("Context").GetComponent<Context>();
            workTasksAvailable = 0;
            isWorkAvailable = false;
            workAdded = false;
            RefreshActions();
        }

        // Update is called once per frame
        void Update()
        {
            IsWorkAvailable();
            RefreshIfWorkAvailable();
        }

        private void RefreshIfWorkAvailable()
        {
            if (isWorkAvailable)
            {
                if (!workAdded)
                {
                    RefreshActions();
                    workAdded = true;
                }
            }

            if (!isWorkAvailable)
            {
                if (workAdded)
                {
                    RefreshActions();
                    workAdded = false;
                }
            }
        }

        private void IsWorkAvailable()
        {
            if (workTasksAvailable > 0)
            {
                isWorkAvailable = true;
            }
            else
            {
                isWorkAvailable = false;
            }
        }

        public void AddWorkTask(int amount)
        {
            workTasksAvailable += amount;
        }

        //Adding work every time it's refreshed - multiples?
        public void RefreshActions()
        {
            if (isWorkAvailable)
            {
                aiBrain.actionsAvailable.Add(work);
            }
            else
            {
                foreach (Action action in aiBrain.actionsAvailable)
                {
                    if (action.Name == work.Name)
                    {
                        Debug.Log("Found Work...");
                        aiBrain.actionsAvailable.Remove(action);
                    }
                }
            }


            //Add sleep action or remove



        }
    }
}

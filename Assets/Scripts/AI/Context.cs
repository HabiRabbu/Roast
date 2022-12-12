using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.UtilityAI;

namespace TL.Core
{
    public class Context : MonoBehaviour
    {
        public ActionManager actionManager;
        
        public string storageTag = "storage";
        public string sleepTag = "sleep";
        //public string coffeePlantTag = "coffeePlant";
        public float MinDistance = 20f;
        public Dictionary<DestinationType, List<Transform>> Destinations { get; private set; }

        void Start()
        {
            actionManager = GameObject.Find("ActionManager").GetComponent<ActionManager>();
            Destinations = new Dictionary<DestinationType, List<Transform>>();
            RefreshDestinations();
        }

        public void RefreshDestinations()
        {
            List<Transform> sleepDestinations = GetAllBeds();
            List<Transform> storageDestinations = GetAllStorage();
            //List<Transform> coffeePlantDestinations = GetAllCoffeePlants();
            List<Transform> workObjectDestinations = GetAllWorkObjects();

            if (Destinations.Count > 0)
            {
                Destinations.Clear();
            }
            Destinations.Add(DestinationType.sleep, sleepDestinations);
            Destinations.Add(DestinationType.storage, storageDestinations);
            //Destinations.Add(DestinationType.coffeePlant, coffeePlantDestinations);
            Destinations.Add(DestinationType.workObject, workObjectDestinations);
        }
        public Transform GetClosestSleepDestination(NPCController npc)
        {
            Transform closestTransform = null;
            foreach (Transform transform in GetAllBeds())
            {
                if (closestTransform == null)
                {
                    closestTransform = transform;
                }
                if(Vector3.Distance(transform.position, npc.transform.position) < Vector3.Distance(closestTransform.position, npc.transform.position))
                {
                    closestTransform = transform;
                }
            }
            return closestTransform;
        }

        private List<Transform> GetAllStorage()
        {
            Transform[] gameObjects = FindObjectsOfType<Transform>() as Transform[];
            List<Transform> storage = new List<Transform>();
            foreach (Transform go in gameObjects)
            {
                if (go.gameObject.tag == storageTag)
                {
                    storage.Add(go);
                }
            }
            return storage;
        }

        private List<Transform> GetAllWorkObjects()
        {
            WorkObject[] workObjects = FindObjectsOfType<WorkObject>();
            List<Transform> workObjectTransforms = new List<Transform>();
            foreach(WorkObject workObject in workObjects)
            {
                workObjectTransforms.Add(workObject.transform);
            }
            return workObjectTransforms;
        }

        //private List<Transform> GetAllCoffeePlants()
        //{
        //    Transform[] gameObjects = FindObjectsOfType<Transform>() as Transform[];
        //    List<Transform> coffeePlants = new List<Transform>();
        //    foreach (Transform go in gameObjects)
        //    {
        //        if (go.gameObject.tag == coffeePlantTag)
        //        {
        //            coffeePlants.Add(go);
        //        }
        //    }
        //    return coffeePlants;
        //}

        private List<Transform> GetAllBeds()
        {
            Transform[] gameObjects = FindObjectsOfType<Transform>() as Transform[];
            List<Transform> beds = new List<Transform>();
            foreach (Transform go in gameObjects)
            {
                if (go.gameObject.tag == sleepTag)
                {
                    beds.Add(go);
                }
            }
            return beds;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoidsSpace
{
    public class Utility
    {
        public static Vector3 RandomVector3
        {
            get
            {
                var x = Random.Range(-1, 1);
                var y = Random.Range(-1, 1);
                var z = Random.Range(-1, 1);
                var newV = new Vector3(x, y, z);
                while (newV.magnitude == 0)
                {
                    newV = RandomVector3;
                }
                return newV;
            }
        }
    }
    public class GameController : MonoBehaviour
    {
        public GameObject agentPrefab;
        [Range(10, 50)] public int BoidSize;
        public static int Count;
        private UIController _uiController;
        public static List<AgentBehaviour> AgentBehaviours;
        public static List<Agent> Agents;
        public static bool ToggleAvoidBool;
        public void Start()
        {
            _uiController = GetComponent<UIController>();
            ToggleAvoidBool = false;
            BoidSize = 25;
            Count = 0;
            Create();
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
        #region Helpers
        [ContextMenu("Create")]
        public void Create()
        {
            Agents = new List<Agent>();
            AgentBehaviours = new List<AgentBehaviour>();
            for (var i = 0; i < Count; i++)
            {
                var go = Instantiate(agentPrefab);
                go.transform.SetParent(transform);
                go.name = string.Format("{0} {1}", "Agent: ", i);
                go.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                go.transform.localScale = new Vector3(BoidSize, BoidSize, BoidSize);

                var behaviour = go.AddComponent<BoidBehaviour>();
                var boid = ScriptableObject.CreateInstance<Boid>();
                boid.name = go.name;

                Agents.Add(boid);
                AgentBehaviours.Add(behaviour);
                behaviour.SetBoid(boid);
            }
        }
        [ContextMenu("Destroy")]
        public void Destroy()
        {
            foreach (var v in AgentBehaviours)
            {
                DestroyImmediate(v.gameObject);
            }
            Count = 0;
            Agents.Clear();
            AgentBehaviours.Clear();
        }
        [ContextMenu("Add")]
        public void Add(int amount)
        {
            foreach (var v in AgentBehaviours)
            {
                DestroyImmediate(v.gameObject);
            }
            Agents.Clear();
            AgentBehaviours.Clear();
            Count += amount;
            Create();
        }

        [ContextMenu("Avoid Object Toggle")]
        public void ToggleAvoid()
        {
            ToggleAvoidBool = !ToggleAvoidBool;
            _uiController.AvoidText.text = ToggleAvoidBool == false ? "Avoid Object = Off" : "Avoid Object = On";
        }

        public void RealRefresh()
        {
            foreach (var v in AgentBehaviours)
            {
                DestroyImmediate(v.gameObject);
            }
            Agents.Clear();
            AgentBehaviours.Clear();
            var temp = Count;
            Count = temp;
            Create();
        }
        #endregion Helpers
    }
}
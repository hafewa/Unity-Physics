using System.Collections;
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
    //GameController is really the Factory that will create boids
    public class AgentFactory : MonoBehaviour
    {        
        public GameObject agentPrefab;
        
        public static List<AgentBehaviour> AgentBehaviours = new List<AgentBehaviour>();
        public static List<Agent> Agents = new List<Agent>();
        public static bool ToggleAvoidBool;
        public FloatVariable Count;
        public void Start()
        {            
            ToggleAvoidBool = false;
            
            Count.Value = 0;
            
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
            for (var i = 0; i < Count.Value; i++)
            {
                var go = Instantiate(agentPrefab, transform);                
                go.name = string.Format("{0} {1}", "Agent: ", i);                

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
            if (AgentBehaviours.Count < 1) return;
            foreach (var v in AgentBehaviours)
            {
                DestroyImmediate(v.gameObject);
            }            
            Agents.Clear();
            AgentBehaviours.Clear();
            Count.Value = Agents.Count;
        }        

        [ContextMenu("Add")]
        public void Add(int amount)
        {
            Count.Value += amount;
            Create();
        }

        [ContextMenu("Avoid Object Toggle")]
        private void ToggleAvoid()
        {
            ToggleAvoidBool = !ToggleAvoidBool;
            //_uiController.AvoidText.text = ToggleAvoidBool == false ? "Avoid Object = Off" : "Avoid Object = On";
        }

        private void RealRefresh()
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
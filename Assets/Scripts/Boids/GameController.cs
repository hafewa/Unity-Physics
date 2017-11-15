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
    public class GameController : MonoBehaviour
    {
        public GameObject agentPrefab;
        public int Count;
        public Slider Dis, Coh, Align, Boundry, MaxSpeed;
        public Text BoidsText, AvoidText;
        public static List<AgentBehaviour> AgentBehaviours;
        public static List<Agent> Agents;
        public static bool ToggleAvoidBool;
        public void Start()
        {
            ToggleAvoidBool = false;
            AvoidText.text = "Avoid Object = Off";
            Count = 0;
            Create();
            BoidsText.text = "Boids: " + Count;
            Boundry.value = BoidBehaviour.BoundaryDist;
        }
        public void Update()
        {
            BoidsText.text = "Boids: " + Count;
            BoidBehaviour.DFac = Dis.value;
            BoidBehaviour.CFac = Coh.value;
            BoidBehaviour.AFac = Align.value;
            BoidBehaviour.BoundaryDist = Boundry.value;
            foreach (var agent in Agents)
            {
                agent.MaxSpeed = MaxSpeed.value;
            }
        }
        #region Helpers
        [ContextMenu("Create")]
        public void Create()
        {
            MaxSpeed.gameObject.SetActive(true);
            Dis.gameObject.SetActive(true);
            Coh.gameObject.SetActive(true);
            Align.gameObject.SetActive(true);
            Agents = new List<Agent>();
            AgentBehaviours = new List<AgentBehaviour>();
            for (var i = 0; i < Count; i++)
            {
                //var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                var go = Instantiate(agentPrefab) as GameObject;
                go.transform.SetParent(transform);
                go.name = string.Format("{0} {1}", "Agent: ", i);
                go.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                go.transform.localScale = new Vector3(50f, 50f, 50f);

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
            MaxSpeed.gameObject.SetActive(false);
            Dis.gameObject.SetActive(false);
            Coh.gameObject.SetActive(false);
            Align.gameObject.SetActive(false);
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
            ToggleAvoidBool = ToggleAvoidBool == false;
            AvoidText.text = ToggleAvoidBool == false ? "Avoid Object = Off" : "Avoid Object = On";
        }
        #endregion Helpers
    }
}
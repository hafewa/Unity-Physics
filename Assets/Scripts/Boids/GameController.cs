using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Donray
{
    public class Utility
    {
        public static Vector3 RandomVector3
        {
            get
            {
                var x = UnityEngine.Random.Range(-1, 1);
                var y = UnityEngine.Random.Range(-1, 1);
                var z = UnityEngine.Random.Range(-1, 1);
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
        public float kc, kd, ka;
        public int Count;
        public Slider Dis, Coh, Align;
        public static List<AgentBehaviour> AgentBehaviours;
        public static List<Agent> Agents;

        public void Start()
        {
            Dis.gameObject.SetActive(false);
            Coh.gameObject.SetActive(false);
            Align.gameObject.SetActive(false);
            Create();
        }
        public void Update()
        {
            BoidBehaviour.DFac = Dis.value;
            BoidBehaviour.CFac = Coh.value;
            BoidBehaviour.AFac = Align.value;

            kc = BoidBehaviour.CFac;
            kd = BoidBehaviour.DFac;
            ka = BoidBehaviour.AFac;
        }
        #region Helpers
        [ContextMenu("Create")]
        public void Create()
        {
            Dis.gameObject.SetActive(true);
            Coh.gameObject.SetActive(true);
            Align.gameObject.SetActive(true);
            Agents = new List<Agent>();
            AgentBehaviours = new List<AgentBehaviour>();
            for (var i = 0; i < Count; i++)
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.SetParent(transform);
                go.name = string.Format("{0} {1}", "Agent: ", i);
                go.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                go.AddComponent<Rigidbody>();
                go.AddComponent<SphereCollider>();
                go.GetComponent<SphereCollider>().isTrigger = true;
                Debug.Log(go.GetComponent<SphereCollider>().isTrigger);

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
            Agents.Clear();
            AgentBehaviours.Clear();
            Dis.gameObject.SetActive(false);
            Coh.gameObject.SetActive(false);
            Align.gameObject.SetActive(false);
        }
#endregion Helpers
    }
}
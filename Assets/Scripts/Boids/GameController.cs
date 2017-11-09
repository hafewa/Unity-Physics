using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public int Count;

        public static List<AgentBehaviour> AgentBehaviours;
        public static List<Agent> Agents;
        [ContextMenu("Create")]
        public void Create()
        {
            Agents = new List<Agent>();
            AgentBehaviours = new List<AgentBehaviour>();
            for (var i = 0; i < Count; i++)
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.SetParent(transform);
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
            foreach (var v in AgentBehaviours)
            {
                DestroyImmediate(v.gameObject);
            }
            Agents.Clear();
            AgentBehaviours.Clear();
        }

    }
}
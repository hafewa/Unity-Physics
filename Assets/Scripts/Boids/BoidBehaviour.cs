using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

namespace Donray
{
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public class BoidBehaviour : AgentBehaviour
    {
        private Agent flocking;
        public static float DFac, AFac, CFac;
        public void Start()
        {
            flocking = ScriptableObject.CreateInstance<Boid>();
        }
        public void SetBoid(Boid b)
        {
            agent = b;
            agent.Create(transform);
        }
        public void Update()
        {
            var boundry = Vector3.zero;
            var dist = Vector3.Distance(transform.position, Vector3.zero);
            if (dist > 10f)
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
                boundry = dist * (Vector3.zero - transform.position);
            }
            agent.AddForce(boundry.magnitude, boundry.normalized);

            var v1 = ((Boid)flocking).Alignment();
            var v2 = ((Boid)flocking).Dispersion();
            var v3 = ((Boid)flocking).Cohesion();
            agent.AddForce(AFac, v1);
            agent.AddForce(DFac, v2);
            agent.AddForce(CFac, v3); 
        }
        public void LateUpdate()
        {
            if (agent == null)
                return;
            transform.position = agent.UpdateAgent(Time.deltaTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Donray
{
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public class BoidBehaviour : AgentBehaviour
    {
        private Agent _flocking;
        public static float DFac, AFac, CFac;
        public static float BoundaryDist;
        public void Start()
        {
            _flocking = ScriptableObject.CreateInstance<Boid>();
            BoundaryDist = 10;
        }
        public void SetBoid(Boid b)
        {
            agent = b;
            agent.Init(transform);
        }
        public void Update()
        {
            var boundary = Vector3.zero;
            var dist = Vector3.Distance(transform.position, Vector3.zero);
            if (dist > BoundaryDist)
            {
                GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 0, 0, 0, 0, 1);

                boundary = dist * (Vector3.zero - transform.position);
            }
            agent.AddForce(boundary.magnitude, boundary.normalized);

            var v1 = ((Boid)_flocking).Alignment(agent as Boid);
            var v2 = ((Boid)_flocking).Dispersion(agent as Boid);
            var v3 = ((Boid)_flocking).Cohesion(agent as Boid);

            agent.AddForce(AFac, v1);
            Debug.DrawLine(agent.Position, agent.Position + v1.normalized, Color.blue);
            agent.AddForce(DFac, v2);
            Debug.DrawLine(agent.Position, agent.Position + v2.normalized, Color.yellow);
            agent.AddForce(CFac, v3);
            Debug.DrawLine(agent.Position, agent.Position + v3.normalized, Color.red);
        }
        public void LateUpdate()
        {
            if (agent == null)
                return;
            transform.position = agent.UpdateAgent(Time.deltaTime);
        }
    }
}
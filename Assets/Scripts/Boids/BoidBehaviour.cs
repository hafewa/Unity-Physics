using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Donray
{
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public class BoidBehaviour : AgentBehaviour
    {
        private FlockingBehaviour _flocking;
        public static float DFac, AFac, CFac;
        public static float BoundaryDist;
        public void Start()
        {
            _flocking = new FlockingBehaviour();
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
                GetComponent<MeshRenderer>().material.color = Color.cyan;

                boundary = dist * (Vector3.zero - transform.position);
            }
            agent.AddForce(boundary.magnitude, boundary.normalized);

            var test = GameObject.FindGameObjectWithTag("test");
            agent.AvoidPos = new Vector3(test.transform.position.x, test.transform.position.y);


            var v1 = (_flocking).Alignment(agent as Boid);
            var v2 = (_flocking).Dispersion(agent as Boid);
            var v3 = (_flocking).Cohesion(agent as Boid);
            var v4 = (_flocking).Avoid(agent as Boid);

            agent.AddForce(AFac, v1);
            Debug.DrawLine(agent.Position, agent.Position + v1.normalized, Color.blue);
            agent.AddForce(DFac, v2);
            Debug.DrawLine(agent.Position, agent.Position + v2.normalized, Color.yellow);
            agent.AddForce(CFac, v3);
            Debug.DrawLine(agent.Position, agent.Position + v3.normalized, Color.red);
            agent.AddForce(10, v4);

            transform.up = agent.Velocity;
        }
        public void LateUpdate()
        {
            if (agent == null)
                return;
            transform.position = agent.UpdateAgent(Time.deltaTime);
        }
    }
}
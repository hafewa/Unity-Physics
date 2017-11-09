using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donray
{
    public class BoidBehaviour : AgentBehaviour
    {
        public float SFac;
        public Boid boid
        {
            get { return (Boid)agent; }
        }
        public void SetBoid(Boid b)
        {
            agent = b;
            agent.OnStart(transform);
        }

        private Vector3 Seperation()
        {
            var flock = GameController.Agents;
            var seperation = Vector3.zero;
            foreach (var bi in flock)
            {
                foreach (var bj in flock)
                {
                    if (bj == this)
                        continue;
                    var dist = Vector3.Distance(bi.position, bj.position);
                    if (dist < 5)
                    {
                        var dir = (bj.position - bi.position).normalized;
                        seperation += dir * SFac;
                    }
                }
            }
            return seperation;
        }
        public void Update()
        {
            var boundray = Vector3.zero;
            var dist = Vector3.Distance(transform.position, Vector3.zero);
            if (dist > 15f)
            {
                GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                boundray = dist * (Vector3.zero - transform.position);
            }
            agent.AddForce(boundray.magnitude, boundray.normalized);
            agent.AddForce(Seperation().magnitude, Seperation().normalized);
        }
        public void LateUpdate()
        {
            if (agent == null)
                return;
            transform.position = agent.UpdateAgent(Time.deltaTime);
        }
    }
}
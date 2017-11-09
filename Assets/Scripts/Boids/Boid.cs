using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donray
{
    public class Boid : Agent
    {
        public override Vector3 UpdateAgent(float deltaTime)
        {
            Acceleration = force * (1 / Mass);
            Velocity += Acceleration * deltaTime;
            Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
            Position += Velocity * deltaTime;
            return Position;
        }

        public List<Agent> Neighbors;

        public void Start()
        {
            Neighbors = GameController.Agents;
        }

        public Vector3 Dispersion()
        {
            Neighbors = GameController.Agents;
            var seperationForce = Vector3.zero;
            if (Neighbors.Count <= 0)
                return Vector3.zero;
            foreach (var neighbor in Neighbors)
            {
                var dist = Vector3.Distance(this.Position, neighbor.Position);

                if (dist < 10f)
                {
                    var dir = (this.Position - neighbor.Position).normalized;
                    seperationForce += dir;
                }   
            }
            return seperationForce;
        }

        public Vector3 Cohesion()
        {
            Neighbors = GameController.Agents;
            var cohesionForce = Vector3.zero;
            
            foreach (var neighbor in Neighbors)
            {
                cohesionForce += neighbor.Position;
            }
            cohesionForce /= Neighbors.Count - 1;
            return cohesionForce;
        }

        public Vector3 Alignment()
        {
            Neighbors = GameController.Agents;
            var alignmentForce = Vector3.zero;
            if (Neighbors.Count <= 0)
                return Vector3.zero;
            foreach (var neighbor in Neighbors)
            {
                alignmentForce += neighbor.Velocity;
            }
            alignmentForce /= Neighbors.Count - 1;
            return alignmentForce;
        }
    }

}
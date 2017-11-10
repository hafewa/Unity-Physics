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
            Acceleration = Force * (1 / Mass);
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

        public Vector3 Dispersion(Boid b)
        {
            Neighbors = GameController.Agents;
            if (Neighbors.Count <= 0)
                return Vector3.zero;
            var seperationForce = Vector3.zero;
            foreach (var neighbor in Neighbors)
            {
                if (neighbor == b) continue;
                var dist = Vector3.Distance(b.Position, neighbor.Position);
                if (dist < 10f)
                {
                    var dir = (b.Position - neighbor.Position).normalized;
                    seperationForce += dir;
                }
            }
            return seperationForce;
        }

        public Vector3 Cohesion(Boid b)
        {
            Neighbors = GameController.Agents;
            if (Neighbors.Count <= 0)
                return Vector3.zero;
            var cohesionForce = Vector3.zero;
            foreach (var neighbor in Neighbors)
            {
                if (neighbor == b) continue;
                cohesionForce += neighbor.Position;
            }
            cohesionForce /= Neighbors.Count;

            return (cohesionForce - b.Position) / 100;
        }

        public Vector3 Alignment(Boid b)
        {
            Neighbors = GameController.Agents;
            if (Neighbors.Count <= 1)
                return Vector3.zero;
            var alignmentForce = Vector3.zero;
            foreach (var neighbor in Neighbors)
            {
                if (neighbor == b) continue;
                alignmentForce += neighbor.Velocity;
            }
            alignmentForce /= Neighbors.Count - 1;
          
            return (alignmentForce - b.Velocity) / 8;
        }
    }

}
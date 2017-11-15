using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Donray
{
    public class FlockingBehaviour : MonoBehaviour
    {
        public static List<Boid> Neighbors(Boid b)
        {
            var neighbors = new List<Boid>();
            var agentss = GameController.Agents.FindAll(x => Vector3.Distance(x.Position, b.Position) < 5);
            agentss.ForEach(a => neighbors.Add(a as Boid));
            return neighbors;
        }

        public Vector3 Avoid(Boid b)
        {
            if (Neighbors(b).Count <= 0)
                return Vector3.zero;
            var avoidForce = Vector3.zero;
            foreach (var neighbor in Neighbors(b))
            {
                var dist = Vector3.Distance(neighbor.AvoidPos, neighbor.Position);
                if (dist < 10)
                {
                    var dir = (neighbor.AvoidPos - neighbor.Position).normalized;
                    avoidForce += dir;
                }
            }
            return -avoidForce;
        }

        public Vector3 Dispersion(Boid b)
        {
            if (Neighbors(b).Count <= 0)
                return Vector3.zero;
            var seperationForce = Vector3.zero;
            foreach (var neighbor in Neighbors(b))
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
            if (Neighbors(b).Count <= 0)
                return Vector3.zero;
            var cohesionForce = Vector3.zero;
            foreach (var neighbor in Neighbors(b))
            {
                if (neighbor == b) continue;
                cohesionForce += neighbor.Position;
            }
            cohesionForce /= Neighbors(b).Count;

            return (cohesionForce - b.Position) / 100;
        }

        public Vector3 Alignment(Boid b)
        {
            if (Neighbors(b).Count <= 1)
                return Vector3.zero;
            var alignmentForce = Vector3.zero;
            foreach (var neighbor in Neighbors(b))
            {
                if (neighbor == b) continue;
                alignmentForce += neighbor.Velocity;
            }
            alignmentForce /= Neighbors(b).Count - 1;

            return (alignmentForce - b.Velocity) / 8;
        }
    }

}
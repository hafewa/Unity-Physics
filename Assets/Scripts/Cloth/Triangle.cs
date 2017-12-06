using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookesLaw
{
    [Serializable]
    public class Triangle
    {
        public float p = 1;
        public float Cd = 1;
        public Particle Particle1, Particle2, Particle3;

        public Triangle(Particle p1, Particle p2, Particle p3)
        {
            Particle1 = p1;
            Particle2 = p2;
            Particle3 = p3;
        }

        public void AerodynamicForce(Vector3 Force)
        {
            var crossA = Particle2.Position - Particle1.Position;
            var crossB = Particle3.Position - Particle1.Position;

            var v = (Particle1.Velocity + Particle2.Velocity + Particle3.Velocity) / 3f - Force;

            var a0 = .5f * Vector3.Cross(crossA.normalized, crossB.normalized).magnitude;

            var n = Vector3.Cross(crossA, crossB.normalized);

            var a = a0 * (Vector3.Dot(v, n) / v.magnitude);

            var force = -.5f * p * (v.magnitude * v.magnitude) * Cd * a * n;

            Particle1.AddForce(force / 3f);
            Particle2.AddForce(force / 3f);
            Particle3.AddForce(force / 3f);
        }
    }
}

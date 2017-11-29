using System;
using UnityEngine;

namespace HookesLaw
{
    [Serializable]
    public class Particle
    {
        public Particle()
        {
            Postion = Vector3.zero;
            Velocity = Vector3.zero;
            Acceleration = Vector3.zero;
            Mass = 1;
        }

        public Particle(Vector3 p, Vector3 v, float m)
        {
            Force = Vector3.zero;
            Mass = m;
            Acceleration = Vector3.zero;
            Velocity = v;
            Postion = p;
        }

        public Vector3 Postion { get; private set; }

        public Vector3 Velocity { get; private set; }

        public Vector3 Acceleration { get; private set; }

        public Vector3 Force { get; private set; }

        // At least 1 and not weight
        public float Mass { get; private set; }

        public bool IsAnchor { get; private set; }

        public void AddForce(Vector3 f)
        {
            Force += f;
        }

        public Vector3 Update(float deltaTime)
        {
            if (IsAnchor)
                return Vector3.zero;
            Acceleration = Force / Mass;
            Velocity += Acceleration * deltaTime;
            Postion += Velocity * deltaTime;
            Force = Vector3.zero;
            IsAnchor = false;
            return Postion;
        }
    }
}
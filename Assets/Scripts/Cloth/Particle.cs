using System;
using UnityEngine;

namespace HookesLaw
{
    [Serializable]
    public class Particle
    {
        [SerializeField] private Vector3 _acceleration;

        [SerializeField] private Vector3 _force;

        [SerializeField] private Vector3 _postion;

        [SerializeField] private Vector3 _velocity;

        // At least 1 and not weight
        public float Mass;
        public bool IsAnchor;
        public bool IsGravity;

        public Vector3 Postion
        {
            get { return _postion; }
            set { _postion = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector3 Acceleration
        {
            get { return _acceleration; }
            set { _acceleration = value; }
        }

        public Vector3 Force
        {
            get { return _force; }
            set { _force = value; }
        }

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

        public void AddForce(Vector3 f)
        {
            Force += f;
        }

        public Vector3 Update(float deltaTime)
        {
            if (IsAnchor)
            {
                Force = Vector3.zero;
                return Postion;
            }
            Acceleration = Force / Mass;
            Force = Vector3.zero;
            Velocity += Acceleration * deltaTime;
            Postion += Velocity * deltaTime;
            return Postion;
        }
    }
}
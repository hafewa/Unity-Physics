using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoidsSpace
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
    }
}
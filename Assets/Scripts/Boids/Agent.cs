using UnityEngine;

namespace Donray
{
    [CreateAssetMenu(fileName = "Agent", menuName = "Variables/Agent", order = 1)]
    public abstract class Agent : ScriptableObject
    {
        [SerializeField] protected Vector3 Acceleration;

        [SerializeField] protected Vector3 force;

        [SerializeField] protected float Mass;

        [SerializeField] protected float MaxSpeed;
        public Transform Owner;

        [SerializeField] private Vector3 position;

        [SerializeField] private Vector3 velocity;

        public Vector3 Position
        {
            get { return position; }

            set { position = value; }
        }

        public Vector3 Velocity
        {
            get { return velocity; }

            set { velocity = value; }
        }

        public abstract Vector3 UpdateAgent(float deltaTime);

        public void Create(Transform owner)
        {
            Owner = owner;
            Mass = 1;
            Velocity = Utility.RandomVector3;
            Acceleration = Vector3.zero;
            Position = owner.position;
            MaxSpeed = 10;
        }

        public bool AddForce(float mag, Vector3 direction)
        {
            if (mag == 0)
                return false;
            var vector = mag * direction;
            force += vector;
            return true;
        }
    }
}
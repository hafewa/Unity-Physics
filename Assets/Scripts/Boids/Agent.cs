using UnityEngine;

namespace Donray
{
    [CreateAssetMenu(fileName = "Agent", menuName = "Variables/Agent", order = 1)]
    public abstract class Agent : ScriptableObject
    {
        [SerializeField] protected Vector3 _position;
        [SerializeField] protected Vector3 _velocity;
        [SerializeField] protected Vector3 Acceleration;
        [SerializeField] private Vector3 avoidPos;
        [SerializeField] protected Vector3 Force;
        [SerializeField] protected float Mass;
        [SerializeField] protected float maxSpeed;
        public Transform Owner;

        public Vector3 Position
        {
            get { return _position; }

            set { _position = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }

            set { _velocity = value; }
        }

        public float MaxSpeed
        {
            get { return maxSpeed; }

            set { maxSpeed = value; }
        }

        public Vector3 AvoidPos
        {
            get { return avoidPos; }

            set { avoidPos = value; }
        }

        public abstract Vector3 UpdateAgent(float deltaTime);

        public void Init(Transform owner)
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
            Force += vector;
            return true;
        }
    }
}
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

        [SerializeField] public Vector3 position;

        [SerializeField] protected Vector3 Velocity;

        public abstract Vector3 UpdateAgent(float deltaTime);
        public Transform Owner;

        protected Vector3 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public void OnStart(Transform owner)
        {
            Owner = owner;
            Mass = 1;
            Velocity = Utility.RandomVector3;
            Acceleration = Utility.RandomVector3;
            Position = owner.position;
            MaxSpeed = 5;
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
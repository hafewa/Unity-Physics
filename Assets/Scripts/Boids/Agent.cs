using UnityEngine;

namespace BoidsSpace
{ 
    public abstract class Agent : ScriptableObject
    {
        
        [SerializeField] private Vector3 avoidPos;
        [SerializeField] protected Vector3 _position;
        [SerializeField] protected Vector3 _velocity;
        [SerializeField] protected Vector3 Acceleration;        
        [SerializeField] protected Vector3 Force;
        [SerializeField] protected float Mass;
        [SerializeField] protected float maxSpeed;
        [SerializeField] protected float maxForce;


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

        public float MaxForce
        {
            get { return maxForce; }

            set { maxForce = value; }
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
        
        public void Init()
        {            
            Mass = 1;
            MaxSpeed = 10;
            maxForce = 10;
            Acceleration = Vector3.zero;
            Velocity = Random.onUnitSphere;            
            Position = Random.onUnitSphere;
            
            
        }

        void OnEnable()
        {
            Init();
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
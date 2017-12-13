using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace BoidsSpace
{
    public class FlockingBehaviour : MonoBehaviour
    {
        public bool isReady = false;
        public FloatVariable AFac;
        public FloatVariable CFac;
        public FloatVariable DFac;
        public FloatVariable MaxSpeed;
        public FloatVariable MaxForce;
        public FloatVariable BoundaryDistance;
        public FloatVariable BFac;
        public FloatVariable Count;
        public Transform target;

        private GameObject mouse;
        [SerializeField]
        private List<Agent> _agents = new List<Agent>();

        public void SetAgents()
        {
            _agents = AgentFactory.Agents;
            isReady = true;
            BFac.Value = 20;
        }

        public List<Boid> Neighbors(Boid b)
        {
            var neighbors = new List<Boid>();
            var agents = _agents.FindAll(x => Vector3.Distance(x.Position, b.Position) < 5);
            agents.ForEach(a => neighbors.Add(a as Boid));
            return neighbors;
        }

        void Start()
        {
            mouse = target.gameObject;
        }
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.P))
            {
                Cursor.visible = Cursor.visible == false;
            }

            if (!Cursor.visible)
                CursorMovement();
            else if (Cursor.visible)
                mouse.SetActive(false);

            if (!isReady) return;
            foreach (var agent in _agents)
            {
                agent.MaxSpeed = MaxSpeed.Value;
                agent.MaxForce = MaxForce.Value;
                var v1 = Alignment(agent as Boid);
                var v2 = Dispersion(agent as Boid);
                var v3 = Cohesion(agent as Boid);
                var v4 = BoundaryForce(agent as Boid);
                var allforces = AFac.Value * v1 + DFac.Value * v2 + CFac.Value * v3 + BFac.Value * v4;

                agent.AddForce(BFac.Value, v4.normalized);
                agent.AddForce(allforces.magnitude, allforces.normalized);
                var avoidObject = GameObject.FindGameObjectWithTag("avoid");
                if (AvoidButton.ToggleAvoidBool)
                {
                    avoidObject.GetComponent<MeshRenderer>().enabled = true;
                    agent.AvoidPos = new Vector3(avoidObject.transform.position.x,
                        avoidObject.transform.position.y,
                        avoidObject.transform.position.z);
                    var v5 = Avoid(agent as Boid);
                    agent.AddForce(20, v5);
                }
                else
                {
                    avoidObject.GetComponent<MeshRenderer>().enabled = false;
                }
               
            }
        }

        private void CursorMovement()
        {
            var move = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 1);
            mouse.SetActive(true);
            mouse.transform.position = (move - Input.mousePosition) * -0.15f;
            mouse.SetActive(Cursor.visible != true);
        }
        #region Algorithm
        public Vector3 Avoid(Boid b)
        {
            var avoidForce = Vector3.zero;
            foreach (var neighbor in Neighbors(b))
            {
                var dist = Vector3.Distance(neighbor.AvoidPos, neighbor.Position);
                if (dist < 5f)
                {
                    var dir = (neighbor.AvoidPos - neighbor.Position).normalized;
                    avoidForce = dist * dir;
                }
            }
            return avoidForce;
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
            if (Neighbors(b).Count <= 1)
                return Vector3.zero;
            var cohesionForce = Vector3.zero;
            foreach (var neighbor in Neighbors(b))
            {
                if (neighbor == b) continue;
                cohesionForce += neighbor.Position;
            }
            cohesionForce /= Neighbors(b).Count - 1;

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

        public Vector3 BoundaryForce(Boid b)
        {
            var force = Vector3.zero;
            var dist = Vector3.Distance(b.Position, target.position);
            if (dist > BoundaryDistance.Value)
                force = dist  * (target.position - b.Position);
            return force;
        }
    }
    #endregion Algorithm

}
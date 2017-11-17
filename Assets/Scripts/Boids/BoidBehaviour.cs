using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace BoidsSpace
{
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public class BoidBehaviour : AgentBehaviour
    {
        //public static float DFac, AFac, CFac;
        //public static float BoundaryDist;
        private List<Color> _colorList;
        //private Color _randomColor;
        private static int i;
        public void Start()
        {
            i = 0;
            _colorList = new List<Color> { Color.red, Color.blue, Color.cyan, Color.green, Color.grey, Color.yellow, Random.ColorHSV() };
            //_randomColor = _colorList[Random.Range(0, _colorList.Count - 1)];
        }

        public void SetBoid(Boid b)
        {
            agent = b;
        }

        public void LateUpdate()
        {
            if (agent == null)
                return;
            transform.localPosition = agent.UpdateAgent(Time.deltaTime);
            transform.forward = agent.Velocity.normalized;
            SetColor();
        }

        public void SetColor()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (i == _colorList.Count - 1)
                    i = 0;
                i++;
                GetComponentInChildren<MeshRenderer>().material.color = _colorList[i];
            }
        }
        //public void _Update()
        //{
        //    var boundary = Vector3.zero;
        //    var dist = Vector3.Distance(transform.position, Vector3.zero);
        //    if (dist > BoundaryDist)
        //        boundary = dist * (Vector3.zero - transform.position);
        //    agent.AddForce(boundary.magnitude, boundary.normalized);
        //    DoMath();
        //}



        //  private void DoMath()
        //    {
        //        if (GameController.ToggleAvoidBool)
        //        {
        //            var avoidObject = GameObject.FindGameObjectWithTag("avoid");
        //            avoidObject.GetComponent<MeshRenderer>().enabled = true;
        //            agent.AvoidPos = new Vector3(avoidObject.transform.position.x,
        //                avoidObject.transform.position.y,
        //                avoidObject.transform.position.z);
        //            var dist = Vector3.Distance(transform.position, agent.AvoidPos);
        //            if (dist < 5f) GetComponent<MeshRenderer>().material.color = Color.red;
        //            else GetComponent<MeshRenderer>().material.color = Color.green;
        //            var v4 = FlockingBehaviour.Avoid(agent as Boid);
        //            agent.AddForce(3000, v4);
        //        }
        //        else
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                if (i == _colorList.Count - 1)
        //                    i = 0;
        //                i++;
        //                GetComponent<MeshRenderer>().material.color = _colorList[i];
        //            }
        //            var avoidObject = GameObject.FindGameObjectWithTag("avoid");
        //            avoidObject.GetComponent<MeshRenderer>().enabled = false;
        //        }

        //        var v1 = FlockingBehaviour.Alignment(agent as Boid);
        //        var v2 = FlockingBehaviour.Dispersion(agent as Boid);
        //        var v3 = FlockingBehaviour.Cohesion(agent as Boid);

        //        agent.AddForce(AFac, v1);
        //        Debug.DrawLine(agent.Position, agent.Position + v1.normalized, Color.blue);
        //        agent.AddForce(DFac, v2);
        //        Debug.DrawLine(agent.Position, agent.Position + v2.normalized, Color.yellow);
        //        agent.AddForce(CFac, v3);
        //        Debug.DrawLine(agent.Position, agent.Position + v3.normalized, Color.red);
        //        transform.up = agent.Velocity;
        //    }
        //}
    }
}

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace BoidsSpace
{
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public class BoidBehaviour : AgentBehaviour
    {
        private List<Color> _colorList;
        private static int i;
        public void Start()
        {
            i = 0;
            _colorList = new List<Color> { Color.red, Color.blue, Color.cyan, Color.green, Color.grey, Color.yellow, Random.ColorHSV() };
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
    }
}

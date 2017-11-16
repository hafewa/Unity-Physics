using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming
namespace BoidsSpace
{
    public class UIController : MonoBehaviour
    {
        public Slider MaxSpeedSL, DispersionSL, CohesionSL, AlignmentSL, BoundrySL;
        public Text BoidsText, AvoidText;

        // Use this for initialization
        void Start()
        {
            AvoidText.text = "Avoid Object = Off";
            BoidsText.text = "Boids: " + GameController.Count;
            BoundrySL.value = BoidBehaviour.BoundaryDist;
            BoundrySL.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            BoidsText.text = "Boids: " + GameController.Count;
            BoidBehaviour.DFac = DispersionSL.value;
            BoidBehaviour.CFac = CohesionSL.value;
            BoidBehaviour.AFac = AlignmentSL.value;
            BoidBehaviour.BoundaryDist = BoundrySL.value;
            if (GameController.Agents != null)
                foreach (var agent in GameController.Agents)
                {
                    agent.MaxSpeed = MaxSpeedSL.value;
                    BoundrySL.gameObject.SetActive(!(agent.MaxSpeed <= 0));
                }
        }
    }
}
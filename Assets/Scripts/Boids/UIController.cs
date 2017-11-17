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
        public Text BoidsText, AvoidText, SpeedText, DispersionText, CohesionText, AlignmentText, BoundaryText;

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
            DispersionText.text = "Dispersion: " + Mathf.RoundToInt(BoidBehaviour.DFac);
            BoidBehaviour.CFac = CohesionSL.value;
            CohesionText.text = "Cohesion: " + Mathf.RoundToInt(BoidBehaviour.CFac);
            BoidBehaviour.AFac = AlignmentSL.value;
            AlignmentText.text = "Alignment: " + Mathf.RoundToInt(BoidBehaviour.AFac);
            BoidBehaviour.BoundaryDist = BoundrySL.value;
            BoundaryText.text = "Boundary: " + Mathf.RoundToInt(BoidBehaviour.BoundaryDist);
            if (GameController.Agents != null)
                foreach (var agent in GameController.Agents)
                {
                    agent.MaxSpeed = MaxSpeedSL.value;
                    BoundrySL.gameObject.SetActive(!(agent.MaxSpeed <= 0));
                    SpeedText.text = "Max Speed: " + Mathf.RoundToInt(agent.MaxSpeed);
                }
        }
    }
}
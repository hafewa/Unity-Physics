//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//// ReSharper disable InconsistentNaming
//namespace BoidsSpace
//{
//    public class UIController : MonoBehaviour
//    {
//        public Slider MaxSpeedSL, DispersionSL, CohesionSL, AlignmentSL, BoundrySL;
//        public Text BoidsText, AvoidText, SpeedText, DispersionText, CohesionText, AlignmentText, BoundaryText;
//        public FloatVariable DFac;
//        public FloatVariable AFac;
//        public FloatVariable CFac;
//        public FloatVariable BoundaryDist;
//        // Use this for initialization
//        void Start()
//        {

//            BoidsText.text = "Boids: " + GameController.Count;

//            BoundrySL.gameObject.SetActive(false);
//        }

//        // Update is called once per frame
//        void Update()
//        {
//            BoidsText.text = "Boids: " + GameController.Count;
//            DFac.Value = DispersionSL.value;
//            DispersionText.text = "Dispersion: " + Mathf.RoundToInt(DFac.Value);
//            CFac.Value = CohesionSL.value;
//            CohesionText.text = "Cohesion: " + Mathf.RoundToInt(CFac.Value);
//            AFac.Value = AlignmentSL.value;
//            AlignmentText.text = "Alignment: " + Mathf.RoundToInt(AFac.Value);
//            BoundaryDist.Value = BoundrySL.value;
//            BoundaryText.text = "Boundary: " + Mathf.RoundToInt(BoundaryDist.Value);
//            if (GameController.Agents != null)
//                foreach (var agent in GameController.Agents)
//                {
//                    agent.MaxSpeed = MaxSpeedSL.value;
//                    BoundrySL.gameObject.SetActive(!(agent.MaxSpeed <= 0));
//                    SpeedText.text = "Max Speed: " + Mathf.RoundToInt(agent.MaxSpeed);
//                }
//        }
//    }
//}
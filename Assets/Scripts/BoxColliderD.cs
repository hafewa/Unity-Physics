using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donray
{
    public class BoxColliderD : MonoBehaviour
    {
        public Transform FirstTransform, SecondTransform;
        [SerializeField]
        public AABB_A First;
        [SerializeField]
        public AABB_A Second;

        // Use this for initialization
        void Update()
        {
            First.Min = (FirstTransform.position - FirstTransform.localScale);
            First.Max = (FirstTransform.position + FirstTransform.localScale);
            Debug.DrawLine(First.Min, First.Max, Color.cyan);
            Second.Min = (SecondTransform.position - SecondTransform.localScale) / 5;
            Second.Max = (SecondTransform.position + SecondTransform.localScale) / 5;

            if (Utilites.TestOverLap(First, Second))
                Debug.Log("Worked");
        }
 
    }
}
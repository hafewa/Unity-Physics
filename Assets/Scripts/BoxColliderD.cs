using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donray
{
    public class BoxColliderD : MonoBehaviour
    {
        public Transform FirstTransform, SecondTransform;
        [SerializeField]
        public AABB_A First, Second;
        // Use this for initialization
        void Update()
        {
            Test(First, FirstTransform);
            Test(Second, SecondTransform);
            if (Utilites.TestOverLap(First, Second))
                Debug.Log("Worked");
        }
        static void Test(AABB_A name, Transform transform)
        {
            name.Min = transform.position - (transform.localScale / 2);
            name.Max = transform.position + (transform.localScale / 2);
            Debug.DrawLine(name.Min, name.Max, Color.white);
        }
    }
}
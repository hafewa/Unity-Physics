using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookesLaw
{
    public class GenerateGrid : MonoBehaviour
    {
        public int Size;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(Generate());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator Generate()
        {
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.AddComponent<ParticleBehaviour>();
                    sphere.transform.position = new Vector3(x * 2.5f, y * 2.5f, 0);
                    sphere.transform.parent = transform;
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
}
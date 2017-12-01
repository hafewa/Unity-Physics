using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HookesLaw
{
    public class GenerateGrid : MonoBehaviour
    {
        public int Size;

        private GameObject _sphere;

        private List<ParticleBehaviour> SpheresList;

        public ParticleBehaviour[] verts;
        // Use this for initialization
        private void Start()
        {
            StartCoroutine(Generate());

        }

        // Update is called once per frame
        private void Update()
        {
            if (verts.Length == 25)
                Debug.DrawLine(SpheresList[0].transform.position, SpheresList[1].transform.position);
        }

        private IEnumerator Generate()
        {
            verts = new ParticleBehaviour[Size * Size];
            var i = 0;
            for (var y = 0; y < Size; y++)
            for (var x = 0; x < Size; x++)
            {
                _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _sphere.AddComponent<ParticleBehaviour>();
                SpheresList = FindObjectsOfType<ParticleBehaviour>().ToList();
                verts = SpheresList.ToArray();
                _sphere.transform.position = new Vector3(x * 2.5f, y * 2.5f, 0);
                _sphere.transform.parent = transform;
                _sphere.name = string.Format("{0}{1}", "Particle: ", i++);
                yield return new WaitForSeconds(0.05f);
            }
           
        }
    }
}
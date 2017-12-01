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
        private IEnumerator Generate()
        {
            var size2 = Size * Size;
            verts = new ParticleBehaviour[size2];
            var iD = 0;
            for (var y = 0; y < Size; y++)
            for (var x = 0; x < Size; x++)
            {
                _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                var beh = _sphere.AddComponent<ParticleBehaviour>();

                SpheresList = FindObjectsOfType<ParticleBehaviour>().ToList();
                verts = SpheresList.ToArray();
                _sphere.transform.position = new Vector3(x * 2.5f, y * 2.5f, 0);
                _sphere.transform.parent = transform;
                _sphere.name = string.Format("{0}{1}", "Particle: ", iD++);

                verts[(y * Size + x )] = beh;
                yield return new WaitForSeconds(0.05f);
            }
            for (var i = 0; i < size2 - 1; i++)
            {
                if (i > (Size * (Size - 1)) - 1)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i + 1];
                }
                else if (i % Size == 0)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i + 1];
                }
            }
        }
    }
}
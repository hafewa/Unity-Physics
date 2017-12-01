using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HookesLaw
{
    public class SpringDriver : MonoBehaviour
    {
        public int Size;
        public bool GravityAll;
        public float ks, lo, kd;
        [HideInInspector] public List<ParticleBehaviour> particleBehaviours;
        [HideInInspector] public List<SpringDamperBehavior> springDamperBehaviors;
        [HideInInspector] public ParticleBehaviour[] verts;
        private GameObject _sphere;

        // Use this for initialization
        private void Start()
        {
            ks = 10f;
            lo = 3;
            kd = .5f;
            Generate();

            particleBehaviours = FindObjectsOfType<ParticleBehaviour>().ToList();
            springDamperBehaviors = FindObjectsOfType<SpringDamperBehavior>().ToList();
        }

        // Update is called once per frame
        private void Update()
        {
            foreach (var sd in springDamperBehaviors)
            {
                sd.springConstant = ks;
                sd.restLength = lo;
                sd.springDamper = kd;
            }
            foreach (var p in particleBehaviours)
            {
                if (p.particle.IsGravity)
                    p.particle.AddForce(new Vector3(0, -9.81f, 0));
                //p.particle.IsAnchor = AnchorAll;
                p.particle.IsGravity = GravityAll;
            }
            foreach (var p in springDamperBehaviors)
            {
                p.SpringDot(p.p1.particle, p.p2.particle, ks, lo, kd);
                Debug.DrawLine(p.p1.particle.Postion, p.p2.particle.Postion);
            }
            foreach (var p in particleBehaviours)
                p.UpdateParticle();
        }

        public void Generate()
        {
            var size2 = Size * Size;
            verts = new ParticleBehaviour[size2];
            var iD = 0;
            for (var y = 0; y < Size; y++)
            for (var x = 0; x < Size; x++)
            {
                _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                DestroyImmediate(_sphere.GetComponent<Renderer>());
                var beh = _sphere.AddComponent<ParticleBehaviour>();
                _sphere.transform.position = new Vector3(x * 2.5f, y * 2.5f, 0);
                _sphere.transform.parent = transform;
                _sphere.name = string.Format("{0}{1}", "Particle: ", iD++);
                verts[y * Size + x] = beh;
            }
            iD = 0;
            for (var i = 0; i < size2 - 1; i++)
            {
                //Horizontal
                if (i % Size != Size - 1)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i + 1];
                }
                //Vertical
                if (i < size2 - Size)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i + Size];
                }
                //Diag Top Left - Bot Right
                if (i % Size != 0 && i < size2 - Size)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i - 1 + Size];
                }
                //Diag Bot Right - Top Left
                if (i % Size != Size - 1 && i < size2 - Size)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i + 1 + Size];
                }
                #region Old

                //if ((i + 1) % Size == 0)
                //{
                //    var go = new GameObject();
                //    var sD = go.AddComponent<SpringDamperBehavior>();
                //    go.transform.parent = transform;
                //    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                //    sD.p1 = verts[i];
                //    sD.p2 = verts[i + Size];

                //    var go2 = new GameObject();
                //    var sD2 = go2.AddComponent<SpringDamperBehavior>();
                //    go2.transform.parent = transform;
                //    go2.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                //    sD2.p1 = verts[i];
                //    sD2.p2 = verts[i - 1 + Size];
                //}

                #endregion
            }
        }
    }
}
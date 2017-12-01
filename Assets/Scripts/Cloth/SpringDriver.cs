using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HookesLaw
{
    public class SpringDriver : MonoBehaviour
    {
        private GameObject _sphere;
        public bool GravityAll;
        public float ks, lo, kd;
        [HideInInspector] public List<ParticleBehaviour> pbs = new List<ParticleBehaviour>();
        public int Size;
        private int Size2;
        [HideInInspector] public List<SpringDamperBehavior> sbs = new List<SpringDamperBehavior>();
        public List<Triangle> trianglesList = new List<Triangle>();
        [HideInInspector] public ParticleBehaviour[] verts;

        // Use this for initialization
        private void Start()
        {
            ks = 10f;
            lo = 3;
            kd = .5f;
            Generate();
            pbs = FindObjectsOfType<ParticleBehaviour>().ToList();
            sbs = FindObjectsOfType<SpringDamperBehavior>().ToList();
            for (var i = 0; i < (Size * Size) - Size; i++)
            {
                if (i < Size2 - Size && i % Size != Size - 1)
                {
                    trianglesList.Add(new Triangle(verts[i].particle,
                        verts[i + 1].particle,
                        verts[i + Size].particle));
                    trianglesList.Add(new Triangle(verts[i].particle,
                        verts[i + 1].particle,
                        verts[i + Size + 1].particle));
                }
            }
        }

        // Update is called once per frame
        private void Update()
        {
            foreach (var sd in sbs)
            {
                sd.springConstant = ks;
                sd.restLength = lo;
                sd.springDamper = kd;
            }
            foreach (var p in pbs)
            {
                if (p.particle.IsGravity)
                    p.particle.AddForce(new Vector3(0, -9.81f, 0));
                //p.particle.IsAnchor = AnchorAll;
                p.particle.IsGravity = GravityAll;
            }
            foreach (var p in sbs)
            {
                p.SpringDot(p.p1.particle, p.p2.particle, ks, lo, kd);
                Debug.DrawLine(p.p1.particle.Postion, p.p2.particle.Postion);
            }
            foreach (var p in pbs)
                p.UpdateParticle();
        }

        public void Generate()
        {
            Size2 = Size * Size;
            verts = new ParticleBehaviour[Size2];
            var iD = 0;
            for (var y = 0; y < Size; y++)
                for (var x = 0; x < Size; x++)
                {
                    _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    _sphere.transform.position = new Vector3(x * 2.5f, y * 2.5f, 0);
                    DestroyImmediate(_sphere.GetComponent<Renderer>());
                    var beh = _sphere.AddComponent<ParticleBehaviour>();
                    _sphere.transform.parent = transform;
                    _sphere.name = string.Format("{0}{1}", "Particle: ", iD++);
                    verts[y * Size + x] = beh;
                }
            iD = 0;
            for (var i = 0; i < Size2 - 1; i++)
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
                if (i < Size2 - Size)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i + Size];
                }
                //Diag Top Left - Bot Right
                if (i % Size != 0 && i < Size2 - Size)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i - 1 + Size];
                }
                //Diag Bot Right - Top Left
                if (i % Size != Size - 1 && i < Size2 - Size)
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
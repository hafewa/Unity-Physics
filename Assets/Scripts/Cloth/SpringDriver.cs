using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HookesLaw
{
    public class SpringDriver : MonoBehaviour
    {
        private GameObject _sphere;
        public float GravForce;
        public bool GravityAll, ApplyWind, LockTopLeft, LockTopRight, LockBotLeft, LockBotRight;
        public float ks, kd;
        public Vector3 Wind;
        [HideInInspector]
        public List<ParticleBehaviour> pbs = new List<ParticleBehaviour>();
        public int Size;
        private int Size2;
        [HideInInspector]
        public List<SpringDamperBehavior> sbs = new List<SpringDamperBehavior>();
        public List<Triangle> trianglesList = new List<Triangle>();
        [HideInInspector]
        public ParticleBehaviour[] verts;

        // Use this for initialization
        private void Start()
        {
            Wind = new Vector3(5, 0, 5);
            ks = 10f;
            kd = .5f;
            Generate();
            SetTriangles();
        }

        // Update is called once per frame
        private void Update()
        {
            #region Anchors
            if (LockBotLeft)
                verts[0].particle.IsAnchor = true;
            else if (!LockBotLeft)
                verts[0].particle.IsAnchor = false;

            if (LockBotRight)
                verts[Size - 1].particle.IsAnchor = true;
            else if (!LockBotRight)
                verts[Size - 1].particle.IsAnchor = false;

            if (LockTopLeft)
                verts[Size2 - Size].particle.IsAnchor = true;
            else if (!LockTopLeft)
                verts[Size2 - Size].particle.IsAnchor = false;

            if (LockTopRight)
                verts[Size2 - 1].particle.IsAnchor = true;
            else if (!LockTopRight)
                verts[Size2 - 1].particle.IsAnchor = false;
            #endregion

            foreach (var sd in sbs)
            {
                sd.springConstant = ks;
                sd.springDamper = kd;
            }
            foreach (var p in pbs)
            {
                if (p.particle.IsGravity)
                    p.particle.AddForce(new Vector3(0, -9.81f, 0) * GravForce);
                p.particle.IsGravity = GravityAll;
            }

            foreach (var triangle in trianglesList)
            {
                if (ApplyWind)
                    triangle.AerodynamicForce(Wind);
            }
            var removedList = new List<SpringDamperBehavior>();
            foreach (var sd in sbs)
            {
                sd.SpringDot(sd.p1.particle, sd.p2.particle, ks, kd);
                if (sd.DistanceBreak())
                    removedList.Add(sd);
            }
            foreach (var p in removedList)
            {
                if (sbs.Contains(p))
                    sbs.Remove(p);
                DestroyImmediate(p.gameObject);
            }
            foreach (var p in sbs)
            {
                if (p == null)
                    break;
                p.SpringDot(p.p1.particle, p.p2.particle, ks, kd);
                //Debug.DrawLine(p.p1.particle.Position, p.p2.particle.Position);
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
                    _sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    _sphere.transform.position = new Vector3(x * 2.5f, y * 2.5f, 0);
                    //DestroyImmediate(_sphere.GetComponent<Renderer>());
                    DestroyImmediate(_sphere.GetComponent<BoxCollider>());
                    var beh = _sphere.AddComponent<ParticleBehaviour>();
                    _sphere.transform.parent = transform;
                    _sphere.name = string.Format("{0}{1}", "Particle: ", iD++);
                    verts[y * Size + x] = beh;
                }
            iD = 0;
            pbs.AddRange(verts);
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


                //Bending Springs
                //Horizontal
                if (i % Size != Size - 1 && i % Size != Size - 2)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i + 2];
                }

                //Vertical
                if (i < Size2 - Size * 2)
                {
                    var go = new GameObject();
                    var sD = go.AddComponent<SpringDamperBehavior>();
                    go.transform.parent = transform;
                    go.name = string.Format("{0}{1}", "SDBehaviour: ", iD++);
                    sD.p1 = verts[i];
                    sD.p2 = verts[i + (Size * 2)];
                }
            }
        }

        public void ResetCloth()
        {
            foreach (var g in pbs)
            {
                DestroyImmediate(g.gameObject);
            }
            foreach (var t in sbs)
            {
                DestroyImmediate(t.gameObject);
            }
            pbs.Clear();
            sbs.Clear();
            trianglesList.Clear();
            Generate();
            SetTriangles();
            foreach (var t in sbs)
                t.Init();
        }

        public void SetTriangles()
        {
           // pbs = FindObjectsOfType<ParticleBehaviour>().ToList();
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
    }
}
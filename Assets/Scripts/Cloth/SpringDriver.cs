using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HookesLaw
{
    public class SpringDriver : MonoBehaviour
    {
        public float ks, lo, kd;
        public List<ParticleBehaviour> particleBehaviours;
        public List<SpringDamperBehavior> springDamperBehaviors;

        // Use this for initialization
        void Start()
        {
            ks = 10f;
            lo = 3;
            kd = .5f;
        }

        // Update is called once per frame
        void Update()
        {
            particleBehaviours = FindObjectsOfType<ParticleBehaviour>().ToList();
            springDamperBehaviors = FindObjectsOfType<SpringDamperBehavior>().ToList();
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
            }
            foreach (var p in springDamperBehaviors)
            {
                p.SpringDot(p.p1.particle, p.p2.particle, ks, lo, kd);
                Debug.DrawLine(p.p1.particle.Postion, p.p2.particle.Postion);
            }
            foreach (var p in particleBehaviours)
            {
                p.UpdateParticle();
            }
        }
    }
}
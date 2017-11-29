using UnityEngine;

namespace HookesLaw
{
    public class SpringDamperBehavior : MonoBehaviour
    {
        public ParticleBehaviour p1, p2;

        private SpringDamper sd;

        // Use this for initialization
        private void Start()
        {
            sd = new SpringDamper(p1.particle, p2.particle, 10f, 3);
        }

        // Update is called once per frame
        private void Update()
        {
            Spring(p1.particle, p2.particle);
            Debug.DrawLine(p1.particle.Postion, p2.particle.Postion);
        }

        public void Spring(Particle a, Particle b)
        {
            var dir = b.Postion - a.Postion;

            var dirNormal = dir.normalized;

            var dist = (a.Postion - b.Postion).magnitude;

            var L = sd._lo - dist;

            var force = -sd._ks * L * dirNormal;

            a.AddForce(force);
            b.AddForce(-force);
        }
    }
}
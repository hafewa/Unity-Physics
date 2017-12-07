using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookesLaw
{
    public class ParticleBehaviour : MonoBehaviour
    {
        [SerializeField]
        public Particle particle;
        // Use this for initialization
        void OnEnable()
        {
            particle = new Particle(transform.position, Vector3.zero, 1);
        }

        // Update is called once per frame
        public void UpdateParticle()
        {
            particle.Position = transform.position;
            particle.Update(Time.deltaTime);
            transform.position = particle.Position;
        }
    }
}

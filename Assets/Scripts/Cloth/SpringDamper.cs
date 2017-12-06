using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookesLaw
{
    [Serializable]
    public class SpringDamper
    {
        public Particle _p1, _p2;

        public float _ks; // Spring Constant 
        public float _lo; // Rest Length 
        public float _kd;

        public SpringDamper()
        {
            
        }
        public SpringDamper(Particle p1, Particle p2, float springConstant, float restLength, float springDamper)
        {
            _p1 = p1;
            _p2 = p2;
            _ks = springConstant;
            _lo = restLength;
            _kd = springDamper;
            _lo = Vector3.Distance(_p1.Position, _p2.Position);
        }
    }
}

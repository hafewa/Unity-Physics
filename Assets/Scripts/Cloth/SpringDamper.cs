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

        public SpringDamper()
        {
            
        }
        public SpringDamper(Particle p1, Particle p2, float springConstant, float restLength)
        {
            _p1 = p1;
            _p2 = p2;
            _ks = springConstant;
            _lo = restLength;
        }
    }
}

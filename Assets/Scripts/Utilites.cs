using System;
using UnityEngine;

namespace Donray
{
    [Serializable]
    public class AABB_A
    { 
        public Vector2 Min;
        public Vector2 Max;
    }

    public static class Utilites
    {
        public static bool TestOverLap(AABB_A a, AABB_A b)
        {
            if (b.Min.x < a.Max.x && b.Max.x > a.Max.x
                || b.Max.x > a.Min.x && b.Min.x < a.Min.x)
                if (b.Min.y < a.Max.y && b.Max.y > a.Max.y
                    || b.Max.y > a.Min.y && b.Min.y < a.Min.x)
                    return true;
            return false;
        }
    }
}
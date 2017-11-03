using System;
using UnityEngine;

namespace Donray
{
    [Serializable]
    public class AABB : ScriptableObject
    {
        public Vector2 Min;
        public Vector2 Max;
        public Transform Owner;
        public int Width, Height;
        public string Name;

        public void UpdateAabb(int width, int height)
        {
            Min.x = Owner.transform.position.x;
            Max.x = Min.x + width;

            Min.y = Owner.transform.position.y;
            Max.y = Min.y + height;
            Draw();
        }
        public void Initialize(int width, int height, Transform owner)
        {
            Width = width;
            Height = height;
            Owner = owner;
            Name = owner.name;
            UpdateAabb(Width, Height);
        }
        public void Draw()
        {
            var bl = new Vector2(Min.x, Min.y);
            var br = new Vector2(Max.x, Min.y);
            var tr = new Vector2(Max.x, Max.y);
            var tl = new Vector2(Min.x, Max.y);
            //Debug.DrawLine(bl, new Vector2(bl.x, 0));
            Debug.DrawLine(bl, tr,Color.cyan);
            Debug.DrawLine(tl, br, Color.cyan);

            Debug.DrawLine(bl, br, Color.cyan);
            Debug.DrawLine(bl, tl, Color.cyan);
            Debug.DrawLine(br, tr, Color.cyan);
            //Debug.DrawLine(br, new Vector2(br.x, 0));
            Debug.DrawLine(tl, tr, Color.cyan);
        }
    }

    public static class Utilites
    {
        public static bool TestOverLap(AABB a, AABB b)
        {
            if (b.Min.x < a.Max.x && b.Max.x > a.Max.x
                || b.Max.x > a.Min.x && b.Min.x < a.Min.x)
                if (b.Min.y < a.Max.y && b.Max.y > a.Max.y
                    || b.Max.y > a.Min.y && b.Min.y < a.Min.x)
                    return true;
            return false;
        }

        public static bool OverLap(AABB a, AABB b)
        {
            var d1x = b.Min.x - a.Max.x;
            var d1y = b.Min.y - a.Max.y;

            var d2x = a.Min.x - b.Max.x;
            var d2y = a.Min.y - b.Max.y;

            if (d1x > 0 || d1y > 0)
                return false;
            if (d2x > 0 || d2y > 0)
                return false;
            return true;
        }
    }
}
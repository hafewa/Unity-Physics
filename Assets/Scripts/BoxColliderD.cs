using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donray
{
    public class BoxColliderD : MonoBehaviour
    {
        [SerializeField]
        public AABB collider;

        public int Width, Height;

        void Start()
        {
            collider = ScriptableObject.CreateInstance<AABB>();
            collider.Initialize(Width, Height, this.transform);
        }
        // Use this for initialization
        void Update()
        {
            collider.UpdateAabb(Width, Height);
        }
    }
}
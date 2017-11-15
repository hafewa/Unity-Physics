using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision

{
    public class BoxColliderD : MonoBehaviour
    {
        [SerializeField]
        public AABB collider;

        public int Width, Height;

        public void Start()
        {
            collider = ScriptableObject.CreateInstance<AABB>();
            collider.Initialize(Width, Height, this.transform);
        }
        // Use this for initialization
        public void Update()
        {
            collider.UpdateAabb(Width, Height);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable InvertIf

namespace TrelloTickets
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public float Speed;
        private Rigidbody _rb;
        [SerializeField]
        private Vector3 input;

        public float accel;
        public Queue<float> InputQueue;

        public List<float> InputList;
        // Use this for initialization
        void Start()
        {
            Speed = 20;
            _rb = GetComponent<Rigidbody>();
            InputQueue = new Queue<float>();
        }

        // Update is called once per frame
        void Update()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");
            input = new Vector3(moveHorizontal, 0.0f, moveVertical);
            
            if (input.magnitude > 0)
            {
                accel += Time.deltaTime;
                accel = Mathf.Clamp(accel, 0, Speed);

                var rotation = Quaternion.LookRotation(input, transform.up);
                //var newrotation = Quaternion.Slerp(transform.rotation, rotation, 20);
                //var angle = Quaternion.Dot(rotation, newrotation);

                _rb.velocity = input * Speed * accel;
                transform.rotation = rotation;

                if (InputQueue.Count > 19)
                    InputQueue.Dequeue();
                InputQueue.Enqueue(input.magnitude);
                InputList = new List<float>(InputQueue);
            }
            else
            {
                accel -= Time.deltaTime;
                accel = Mathf.Clamp(accel, 0, Speed);
                _rb.velocity = input * Speed * accel;
            }
        }
    }
}

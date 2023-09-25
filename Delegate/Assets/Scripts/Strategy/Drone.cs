using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Chapter.Strategy
{
    public class Drone : MonoBehaviour
    {
        //광선 파라미터
        private RaycastHit _hit;
        private Vector3 _rayDirection;
        private float _rayAngle = -45f;
        private float _rayDistance = 15f;

        //이동 파라미터
        public float speed = 1f;
        public float maxHeight = 5f;
        public float weavingDistance = 1.5f;
        public float fallbackDistance = 20f;

        // Start is called before the first frame update
        void Start()
        {
            _rayDirection = transform.TransformDirection(Vector3.back) * _rayDistance;
            _rayDirection = Quaternion.Euler(_rayAngle, 0f, 0f) * _rayDirection;
        }

        public void ApplyStrategy(IManeuverBehaviour strategy)
        {
            strategy.Maneuver(this);
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawRay(transform.position, _rayDirection, Color.blue);

            if(Physics.Raycast(transform.position, _rayDirection, out _hit, _rayDistance))
            {
                if(_hit.collider)
                {
                    Debug.DrawRay(transform.position, _rayDirection, Color.green);
                }
            }
        }
    }
}


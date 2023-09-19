using Chapter.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Observer
{
    public class CameraController : Observer
    {
        private bool _isTurboOn;
        private Vector3 _initialPosition;
        private float _shakeMagnitude = 0.1f;
        private BikeController _bikeConroller;

        private void OnEnable()
        {
            _initialPosition = gameObject.transform.localPosition;
        }

        private void Update()
        {
            if (_isTurboOn)
            {
                gameObject.transform.localPosition = _initialPosition + (Random.insideUnitSphere * _shakeMagnitude);
            }
            else
                gameObject.transform.localPosition = _initialPosition;
        }

        public override void Notify(Subject subject)
        {
            if (!_bikeConroller)
            {
                _bikeConroller = subject.GetComponent<BikeController>();
            }

            if (_bikeConroller)
            {
                _isTurboOn = _bikeConroller.IsTurboOn;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Observer
{
    public class HUDController : Observer 
    {
        private bool _isTurboOn;
        private float _currentHealth;
        private BikeController _bikeConroller;

        private void OnGUI()
        {
            GUILayout.BeginArea(
                new Rect(50, 50, 100, 200));
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Health: " + _currentHealth);
            GUILayout.EndHorizontal();

            if(_isTurboOn)
            {
                GUILayout.BeginHorizontal("Box");
                GUILayout.Label("Turbo Activated!");
                GUILayout.EndHorizontal();
            }

            if(_currentHealth <= 50.0f)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label("WARNING: Low Health");
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();

        }

        public override void Notify(Subject subject)
        {
            if(!_bikeConroller)
            {
                _bikeConroller = subject.GetComponent<BikeController>();
            }

            if(_bikeConroller)
            {
                _isTurboOn = _bikeConroller.IsTurboOn;
                _currentHealth = _bikeConroller.CurrentHealth;
            }
        }
    }

}

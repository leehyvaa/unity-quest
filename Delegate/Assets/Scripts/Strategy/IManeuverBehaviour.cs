using Chapter.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Chapter.Strategy
{
    public interface IManeuverBehaviour
    {
        void Maneuver(Drone drone);
    }


}


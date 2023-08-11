using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IlivingThingsSet
{
    public  int HitPoints{ get; set; }
    public int MoveStack { get; set; } 
    public void Hit(int _value);


}

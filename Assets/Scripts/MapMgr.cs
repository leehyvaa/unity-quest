using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const int MapSizeX = 16;
    public const int MapSizeZ = 9;



}




public class MapMgr : MonoBehaviour
{
    GameObject[,] ground = new GameObject[Constants.MapSizeZ,Constants.MapSizeX];
    GameObject grayGround;
    GameObject whiteGround;

    // Start is called before the first frame update
    void Start()
    {

        grayGround = Resources.Load<GameObject>("Prefabs/GrayGround");
        whiteGround = Resources.Load<GameObject>("Prefabs/WhiteGround");
        

        for (int z = 0; z < Constants.MapSizeZ; z++)
        {
            for (int x = 0; x < Constants.MapSizeX; x++)
            {
                if((z+x) % 2 == 0)
                    ground[z,x]=Instantiate<GameObject>(whiteGround,new Vector3(x,0,z),Quaternion.identity,transform);
                else
                    ground[z,x]=Instantiate<GameObject>(grayGround,new Vector3(x,0,z),Quaternion.identity,transform);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

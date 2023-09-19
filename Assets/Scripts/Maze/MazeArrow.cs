using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeArrow : MonoBehaviour
{
    public Transform coin;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = Vector3.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = coin.position - transform.parent.position;
        dir.Normalize();

        transform.position = transform.parent.position + dir;
        Vector3 coinPos = new Vector3(coin.position.x, transform.position.y, coin.position.z);
        transform.LookAt(coinPos);
    }
}

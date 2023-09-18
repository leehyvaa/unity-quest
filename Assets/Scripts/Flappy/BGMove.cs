using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float moveSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FlappyManager.Instance.gameState != FlappyManager.GameState.Result)
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            if (transform.position.x < -22)
                transform.position = new Vector3(22f, 0, 0f);
        }
    }
}

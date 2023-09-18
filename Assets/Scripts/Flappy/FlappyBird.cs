using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{

    private float jumpPower = 6f;

    Vector3 maxDownDir = new Vector3(0f, 0f, -90f);
    public BirdState birdState;

    public enum BirdState
    {
        Ready,
        Flying,
        Death,
    }

    // Start is called before the first frame update
    void Start()
    {
        birdState = BirdState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        switch (birdState)
        {
            case BirdState.Ready:
                {
                    GetComponent<Rigidbody>().useGravity = false;
                }break;
            case BirdState.Flying:
                {
                    GetComponent<Rigidbody>().useGravity = true;


                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);


                        Vector3 upDir = Vector3.forward;
                        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 55f);
                        transform.rotation = targetRotation;
                        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,1f);

                        
                    }

                    Quaternion targetRot = Quaternion.Euler(maxDownDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.75f * Time.deltaTime);

                }
                break;
            case BirdState.Death:
                {
                    Quaternion targetRot = Quaternion.Euler(maxDownDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.75f * Time.deltaTime);

                }
                break;

        }

        

       
        if(FlappyManager.Instance.gameState == FlappyManager.GameState.Playing)
        {
            birdState = BirdState.Flying;
        }
       




    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Score")
        {
            FlappyManager.Instance.score++;
        }

        if (other.gameObject.tag == "FlappyWall")
        {
            FlappyManager.Instance.gameState = FlappyManager.GameState.Result;

            birdState = BirdState.Death;
           transform.GetComponent<Collider>().enabled = false;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappySpawner : MonoBehaviour
{


    public GameObject wallPrefab;
    public float intervalTime = 1.8f;
    public float offset;
    public float offsetUpper;

    public GameObject upperWall;
    private Vector3 upperWallPos;

    void Start()
    {
        upperWall = wallPrefab.transform.GetChild(0).gameObject;
        upperWallPos = new Vector3(0f, 10f, 0f);
        
        StartCoroutine("WallSpawn");
    }
    // Start is called before the first frame update
    IEnumerator WallSpawn()
    {
        while (true)
        {
            if (FlappyManager.Instance.gameState == FlappyManager.GameState.Playing)
            {
                offsetUpper = Random.Range(0f, 1.5f);
                upperWall.transform.position = new Vector3(upperWallPos.x, upperWallPos.y - offsetUpper, upperWallPos.z);

                offset = Random.Range(-3.5f, 5f);
                Vector3 createPos = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
                Instantiate(wallPrefab, createPos, transform.rotation);

            }

            if (FlappyManager.Instance.gameState== FlappyManager.GameState.Result)
            {
                break;
            }

            yield return new WaitForSeconds(intervalTime);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

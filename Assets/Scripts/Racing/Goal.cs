using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int rank;
    public int count;
    public RacingManager manager;
    // Start is called before the first frame update
    void Start()
    {
        rank = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Car car = other.gameObject.GetComponent<Car>();
            car.lap++;
            count++;

            if (car.lap > RacingManager.maxLap)
            {
                car.go = false;
                car.rank = this.rank++;

                car.lapTime = RacingManager.lapTime;
                car.gameObject.GetComponent<Collider>().enabled = false;
                car.input.y = 0.05f;
            }

            if(count >= (RacingManager.maxLap+2) * 4)
            {
                manager.raceState = RacingManager.Sequence.End;
            }

        }



    }
}

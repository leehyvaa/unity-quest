using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

//이탈복귀, 엔진조작, 바퀴, 충돌, y이동


public class RacingManager : MonoBehaviour
{
    public enum Sequence
    {
        Ready,
        Start,
        Race,
        End,
    }

    static public int maxLap = 1;
    static public string lapTime;


    public Transform[] spawnPoints = new Transform[4];
    public Car[] cars = new Car[4];

    public GameObject Goal;
    public GameObject startBtn;
    public TextMeshProUGUI tmp_Counter;
    public TextMeshProUGUI tmp_LapCount;
    public TextMeshProUGUI tmp_LapTimer;

    public GameObject result;
    public Transform result_Rank;
    public Transform result_Color;
    public Transform result_LapTime;



    public Sequence raceState;

    private float counter;
    private float lapTimer;


    private int lap1;
    private int lap2;
    private int lap3;

    Color[] colors = new Color[4];

    private void Awake()
    {
        colors[0] = Color.white;
        colors[1] = Color.red;
        colors[2] = Color.blue;
        colors[3] = Color.green;


        //플레이어 생성
        GameObject carPrefab = Resources.Load<GameObject>("Prefabs/Car");


        for (int i = 0; i < cars.Length; i++)
        {
            GameObject car = Instantiate<GameObject>(carPrefab, spawnPoints[i].position, transform.rotation, transform);
            cars[i] = car.transform.GetChild(0).GetComponent<Car>();
            cars[i].color = colors[i];


            car.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material.color = colors[i];
        }

        cars[0].auto = false;
        cars[0].gameObject.GetComponent<RacingRay>().enabled = false;

        GameObject camPrefab= Resources.Load<GameObject>("Prefabs/CamArm");
        GameObject cam = Instantiate<GameObject>(camPrefab, cars[0].transform.position, transform.rotation, cars[0].transform.parent);
        cars[0].cameraArm = cam.transform;

    }

    // Start is called before the first frame update
    void Start()
    {
        raceState = Sequence.Ready;
        counter = 3.99f;
        lapTimer = 0f;
        lap1 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(raceState)
        {
            case Sequence.Ready:
                {
                    startBtn.SetActive(true);
                    result.SetActive(false);

                }
                break;
            case Sequence.Start:
                {
                    startBtn.SetActive(false);

                    counter -= Time.deltaTime;
                    tmp_Counter.gameObject.SetActive(true);
                    tmp_LapCount.gameObject.SetActive(true);
                    tmp_LapTimer.gameObject.SetActive(true);



                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                    if (counter >= 1f)
                    {
                        tmp_Counter.text = ((int)counter).ToString();

                    }
                    else
                    {
                        tmp_Counter.text = "Start!";
                        for (int i = 0; i < cars.Length; i++)
                        {
                            cars[i].input.y = 1;
                            cars[i].go = true;
                        }
                        
                        raceState = Sequence.Race;
                    }
                }
                break;
            case Sequence.Race:
                {
                    counter -= Time.deltaTime;
                    lapTimer += Time.deltaTime;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(cars[0].lap.ToString());
                    sb.Append(" / ");
                    sb.Append(maxLap.ToString());
                    tmp_LapCount.text = sb.ToString();

                    StringBuilder sb2 = new StringBuilder();
                    if(lapTimer > 60f)
                    {
                        lap1++;
                        lapTimer = lapTimer - 60f;
                    }
                    lap2 = (int)lapTimer;
                    lap3 = (int)(((Mathf.Floor(lapTimer*100f) / 100f) - lap2)*100);


                    lapTime = string.Format("{0:D2} : {1:D2} : {2:D2}", lap1, lap2, lap3);




                   
                    tmp_LapTimer.text = lapTime.ToString();
                    if (counter <= 0f)
                        tmp_Counter.gameObject.SetActive(false);

                    

                }
                break;
            case Sequence.End:
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    tmp_LapCount.gameObject.SetActive(false);
                    tmp_LapTimer.gameObject.SetActive(false);
                    result.SetActive(true);
                    for (int i = 0; i < cars.Length; i++)
                    {
                        result_Rank.GetChild(cars[i].rank - 1).GetComponent<TextMeshProUGUI>().text = cars[i].rank.ToString();
                        result_Color.GetChild(cars[i].rank - 1).GetComponent<Image>().color = cars[i].color;
                        result_LapTime.GetChild(cars[i].rank - 1).GetComponent<TextMeshProUGUI>().text = cars[i].lapTime;

                    }

                }
                break;
        }
           




    }




    public void RaceStart()
    {
        raceState = Sequence.Start;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CursorControll()
    {
        if (Input.anyKeyDown)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (!Cursor.visible && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSpeed, 0f, Space.World);
        // transform.Rotate(-Input.GetAxis("Mouse Y") * mouseSpeed, 0f, 0f);

    }

    
}

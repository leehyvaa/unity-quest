using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class FlappyManager : MonoBehaviour
{
    private static FlappyManager sInstance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resScoreText;
    public TextMeshProUGUI resBestScoreText;


    public int score;
    public int bestScore;
    public GameState gameState;
    public GameObject UI;
    public GameObject startButton;
    public GameObject restartButton;

    public GameObject result;

    public enum GameState
    {
        Ready,
        Playing,
        Result,
        Restart,
    }

    public static FlappyManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstance = newGameObject.AddComponent<FlappyManager>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sInstance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Setting();
        score = 0;
        bestScore = 0;

    }

    // Update is called once per frame
    void Update()
    {


        switch(gameState)
        {
            case GameState.Ready:
                {
                    startButton.SetActive(true);

                }
                break;
            case GameState.Playing:
                {
                    scoreText.gameObject.SetActive(true);
                     scoreText.text = score.ToString();
                }
                break;
            case GameState.Result:
                {
                    if(bestScore <= score)
                        bestScore = score;
                    scoreText.gameObject.SetActive(false);
                    result.gameObject.SetActive(true);

                    resScoreText.text = score.ToString();
                    resBestScoreText.text = bestScore.ToString();

                }break;
            case GameState.Restart:
                {
                    //Setting();
                }
                break;

        }
        
    }

    public void LateUpdate()
    {

    }

    public void GameStart()
    {
        gameState = GameState.Playing;
        startButton.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        gameState = GameState.Restart;
        score = 0;
 
        Invoke("Setting",0.2f);
    }

    public void Setting()
    {

        UI = GameObject.Find("UI");
        startButton = UI.transform.GetChild(0).GetChild(2).gameObject;
        result = UI.transform.GetChild(0).GetChild(1).gameObject;


        gameState = GameState.Ready;

        scoreText = GameObject.Find("ScorePanel").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        restartButton = result.transform.GetChild(4).gameObject;
        resScoreText = result.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        resBestScoreText = result.transform.GetChild(3).GetComponent<TextMeshProUGUI>();


        startButton.GetComponent<Button>().onClick.AddListener(GameStart);
        restartButton.GetComponent<Button>().onClick.AddListener(Restart);

    }
}

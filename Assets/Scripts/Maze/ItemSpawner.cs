using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Transform coins;
    [SerializeField] Transform playerRoot;
    [SerializeField] Transform enemyRoot;
    [SerializeField] Transform UIRoot;
    

    private List<GameObject> arrCoin = new List<GameObject>();

    private MecanimControl player;
    private Enemy3D enemy;


    private int playerScore = 0;
    private int enemyScore = 0;
    private float countdown;

    private TextMeshProUGUI playerCoinText;
    private TextMeshProUGUI enemyCoinText;
    private TextMeshProUGUI timerText;


    StringBuilder sb = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        
        Init();
        CoinSpawn();
        StartCoroutine("PlayGame");
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;




    }

    IEnumerator PlayGame()
    {

        while (true) 
        {
            if (countdown > 0)
            {
                ScoreUpdate();
                yield return new WaitForSeconds(0.5f);

            }
            else
            {
                GameEnd();
                break;
            }
        }
    }

    void Init()
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Player3D");
        GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy3D");

        player  = Instantiate<GameObject>(playerPrefab, playerRoot.position,transform.rotation, transform).GetComponent<MecanimControl>();
        enemy = Instantiate<GameObject>(enemyPrefab, enemyRoot.position, transform.rotation,transform).GetComponent<Enemy3D>();
        
        

        CoinGetter playerGetter = player.GetComponent<CoinGetter>();
        playerGetter.SetCallback(PlayerGetCoin);


        CoinGetter enemyGetter = enemy.GetComponent<CoinGetter>();
        enemyGetter.SetCallback(EnemyGetCoin);

        enemy.target = player.gameObject;
        enemy.player = player.transform;


        for (int i = 0; i < coins.childCount; i++)
        {
            arrCoin.Add(coins.GetChild(i).gameObject);
            coins.GetChild(i).gameObject.SetActive(false);
        }
        countdown = 30;

        playerCoinText = UIRoot.GetChild(0).GetComponent<TextMeshProUGUI>();
        enemyCoinText = UIRoot.GetChild(1).GetComponent<TextMeshProUGUI>();
        timerText = UIRoot.GetChild(2).GetComponent<TextMeshProUGUI>();

    }

    void ScoreUpdate()
    {
        sb.Clear();
        sb.Append("Player : ");
        sb.Append(playerScore.ToString());
        playerCoinText.text = sb.ToString();

        sb.Clear();
        sb.Append("enemy : ");
        sb.Append(enemyScore.ToString());
        enemyCoinText.text = sb.ToString();


        sb.Clear();
        sb.Append("Timer : ");
        sb.Append(((int)countdown).ToString());
        timerText.text = sb.ToString();
    }

    void GameEnd()
    {
        player.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);


        timerText.gameObject.SetActive(false);

        Transform result = UIRoot.GetChild(3).transform;
        result.gameObject.SetActive(true);
        if(playerScore > enemyScore)
        {
            result.GetChild(0).gameObject.SetActive(true);
        }
        else if (playerScore == enemyScore)
        {
            result.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            result.GetChild(1).gameObject.SetActive(true);
        }

        enemyCoinText.transform.parent =result;
        playerCoinText.transform.parent = result;
        playerCoinText.transform.position =result.position + new Vector3(0, 30, 0);
        enemyCoinText.transform.position = result.position + new Vector3(0, -30, 0);



        Time.timeScale = 0;
    }

    void CoinSpawn()
    {
        int num = Random.Range(0, arrCoin.Count - 1);
        arrCoin[num].gameObject.SetActive(true);

        for (int i = 0; i < arrCoin.Count-1; i++)
        {
            if (arrCoin[i].gameObject.activeSelf)
            {
                enemy.coin = arrCoin[i].transform;
                player.GetComponentInChildren<MazeArrow>().coin = arrCoin[i].transform;
                Debug.Log("coin Set");
            }
        }
        
    }

    void PlayerGetCoin()
    {
        playerScore++;
        CoinSpawn();
    }

    void EnemyGetCoin()
    {
        enemyScore++;
        CoinSpawn();
       
    }

}

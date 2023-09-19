using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player2D;
using UnityEngine.Networking.Types;

public class CoinGetter : MonoBehaviour
{
    public delegate void Callback_OnGetCoin();
    Callback_OnGetCoin onGetCoin;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            if(gameObject.tag == "Player")
            {
                other.gameObject.SetActive(false);
                onGetCoin();
            }
            else if (gameObject.tag =="Enemy")
            {
                other.gameObject.SetActive(false);
                onGetCoin();

            }
        }
    }

    public void SetCallback(Callback_OnGetCoin callback_OnGetCoin)
    {
        if (onGetCoin != null)
        {
            Debug.LogWarning(string.Format("¿ÃπÃ º≥¡§µ  SetCallback() - name : {1} "));
            return;
        }
        onGetCoin = callback_OnGetCoin;

    }


}

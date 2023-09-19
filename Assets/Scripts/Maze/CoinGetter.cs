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
                onGetCoin();
                other.gameObject.SetActive(false);
            }
            else if (gameObject.tag =="Enemy")
            {
                onGetCoin();
                other.gameObject.SetActive(false);

            }
        }
    }

    public void SetCallback(Callback_OnGetCoin callback_OnGetCoin)
    {
        if (onGetCoin != null)
        {
            //Debug.LogWarning(string.Format("[Player ID : {0}] ¿ÃπÃ º≥¡§µ  SetCallback() - name : {1} ", ID, name));
            return;
        }
        onGetCoin = callback_OnGetCoin;

    }


}

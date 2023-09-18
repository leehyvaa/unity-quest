using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefaultUI : MonoBehaviour
{
    public GameObject popupObj = null;

    private void Start()
    {
        if(popupObj)
            popupObj.SetActive(false);
    }

    void onPopup()
    {
        if (popupObj)
        {
            if (popupObj.activeSelf)
            {

                popupObj.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                popupObj.SetActive(true);
            }
        }
        else
            return;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostomNameLogic : MonoBehaviour
{
    private int isBuy = 0;

    [SerializeField] private GameObject nameBttn;
    [SerializeField] private GameObject lockBttn;
    private void Start()
    {
        isBuy = PlayerPrefs.GetInt("CostomNameBuy");

        BuyResult();
    }

    public void Buy()
    {
        if (isBuy == 0 && GameManager.S._tokens >= 10000)
        {
            GameManager.S.Token(-10000);
            isBuy = 1;
            PlayerPrefs.SetInt("CostomNameBuy", isBuy);
            BuyResult();
            Debug.Log("You buy costom name opportunity!");
        }
    }

    public void BuyResult()
    {
        if (isBuy == 1)
        {
            nameBttn.SetActive(true);
            lockBttn.SetActive(false);
        }
        else 
        {
            nameBttn.SetActive(false);
            lockBttn.SetActive(true);
        }
    }
}

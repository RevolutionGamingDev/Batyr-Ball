using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject[] skins;

    [SerializeField] private Button _selectButton;

    [SerializeField] private TextMeshProUGUI _priceText;

    public string _name;

    [SerializeField] private int _price;
    [SerializeField] private int itemId;

    public bool _isSelected;

    private void Start()
    {
        InitializeText();

        if (PlayerPrefs.GetInt(_name) == 1)
        {
            _buyButton.SetActive(false);
        }
    }

    public void BuyItem()
    {
        if (PlayerPrefs.GetInt(_name) == 0)
        {
            if (GameManager.S._tokens >= _price)
            {
                _buyButton.SetActive(false);
                SelectItem();
                GameManager.S.Token(-_price);
                PlayerPrefs.SetInt(_name, 1);
            }

            else
            {
                Debug.Log("Not eough money");
            }
        }

        else
        {
            Debug.Log("This Item is Sold");
        }
    }

    private void Update()
    {
        if(_isSelected) _priceText.text = "Selected";
        if (!_isSelected) _priceText.text = "Select";
        if (!_isSelected && PlayerPrefs.GetInt(_name) == 0) _priceText.text = _price.ToString();
    }
    public void SelectItem()
    {
        Item[] items = FindObjectsOfType<Item>();
        foreach (Item item in items)
        {
            item._isSelected = false;
            item._selectButton.interactable = true;
        }

        _isSelected = true;
        _selectButton.interactable = false;

        if (_isSelected) StartCoroutine(SelectSkin());

            
    }
    private void InitializeText()
    {
        _priceText.text = _price.ToString();
    }
    private IEnumerator SelectSkin()
    {
        for (int num = 0; num < skins.Length; num++)
        {
            skins[num].SetActive(false);
        }
        skins[itemId].SetActive(true);

        yield return false;
    }
    public void DeletAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
//{
//    [SerializeField] private TextMeshProUGUI priceText;

//    //[SerializeField] private Button buy;
//    //[SerializeField] private Button select;

//    public int price;
//    public int isBuy;

//    [SerializeField] private GameObject[] skins;

//    [SerializeField] private string saveName;

//    private void Awake()
//    {
//        isBuy = PlayerPrefs.GetInt(saveName);

//        if (isBuy == 1)
//        {
//            //buy.gameObject.SetActive(false);
//            //select.gameObject.SetActive(true);
//            priceText.text = "Select";
//        }
//        else if(isBuy == 0)
//        {
//            //buy.gameObject.SetActive(true);
//            //select.gameObject.SetActive(false);
//            priceText.text = price.ToString();
//        }
//    }

//    public void Buy()
//    {
//        if(isBuy == 0 && GameManager.S._coin  >= price)
//        {
//            GameManager.S._coin -= price;
//            isBuy = 1;
//            PlayerPrefs.SetInt(saveName, isBuy);
//            //buy.gameObject.SetActive(false);
//            //select.gameObject.SetActive(true);
//            priceText.text = "Select";
//        }
//    }

//    public void Select()
//    {
//        if (isBuy == 1)
//        {
//            StartCoroutine(SelectSkin());
//        }
//    }

//    
//}

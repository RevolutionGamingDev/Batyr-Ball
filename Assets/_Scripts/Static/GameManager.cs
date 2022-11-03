using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    //public TextMeshProUGUI coinText;
    //public TextMeshProUGUI coinTextForShop;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI bitsText;
    public TextMeshProUGUI tokensText;
    public TextMeshProUGUI tokensText2;
    public TMP_InputField nameStroke;
    public TextMeshPro playerName;
    public GameObject leaderboard;
    private int _score;
    //public int _coin;
    [SerializeField] private int _bits;
    public int _tokens;

    private int _highScore;
    private void Awake()
    {
        _highScore = PlayerPrefs.GetInt("highscore");
        _bits = PlayerPrefs.GetInt("Bit");
        _tokens = PlayerPrefs.GetInt("Token");
        //coinText.text = _coin.ToString();
        //coinTextForShop.text = _coin.ToString();
        S = this;
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
        bitsText.text = _bits.ToString();

        ConvertBit();
    }

    //private void Update()
    //{
    //    //UpdateTexts();
    //    //Debug.Log(_bits);
    //}

    public void IncreaseScore()
    {
        _score++;
        //_coin++;
        //PlayerPrefs.SetInt("Coin", _coin);
        scoreText.text = _score.ToString();
        
    }

    public void UpdateScore()
    {
        if (_score > _highScore)
        {
            //Update the highscore sign
            PlayerPrefs.SetInt("highscore", _score);
            highscoreText.text = _score.ToString();
            bitsText.text = _bits.ToString();
            //Update the leaderboard
            //Leaderboard.S.WriteData();
        }
    }

    //public IEnumerator LeaderboardOn()
    //{
    //    yield return new WaitForSeconds(1);
    //    leaderboard.SetActive(true);
    //}
    
    public void SaveName()
    {
        if (nameStroke.text.Length <= 10)
        {
            PlayerPrefs.SetString("Name", nameStroke.text);
            Debug.Log(nameStroke.text);
        }
    }

    public void UpdateName()
    {
        playerName.text = PlayerPrefs.GetString("Name");
    }

    //public void CoinForShop()
    //{
    //    //coinTextForShop.text = _coin.ToString();
    //}s

    //public void SaveCoin()
    //{
    //    //PlayerPrefs.SetInt("Coin", _coin);
    //    //coinTextForShop.text = _coin.ToString();
    //    //coinText.text = _coin.ToString();
    //}

    public void ConvertBit()
    {
        if (PlayerPrefs.GetInt("Bit") < PlayerPrefs.GetInt("highscore"))
        {
            _bits = PlayerPrefs.GetInt("highscore");
            bitsText.text = _bits.ToString();
            PlayerPrefs.SetInt("Bit", _bits);
        }

        Debug.Log("Complete");
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }

    public void UpdateTexts()
    {
        bitsText.text = _bits.ToString();
        //coinText.text = _coin.ToString();
        //coinTextForShop.text = _coin.ToString();
        highscoreText.text = _score.ToString();
        scoreText.text = _score.ToString();
    }

    public void Token(int num)
    {
        _tokens += num;
        tokensText.text = _tokens.ToString();
        tokensText2.text = _tokens.ToString();
        PlayerPrefs.SetInt("Token", _tokens);
    }

    public void ZeroingBit()
    {
        PlayerPrefs.DeleteKey("Bit");
        PlayerPrefs.DeleteKey("highscore");
        bitsText.text = _bits.ToString();
        highscoreText.text = _score.ToString();
        Debug.Log("Zeroing Complete");
    }
}

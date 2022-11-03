using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyCovert : MonoBehaviour
{
    public TextMeshProUGUI Time;
    public float msToWait;
    private ulong lastTimeClicked;

    private void Start()
    {
        lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));
    }

    private void Update()
    {
        if (Ready())
        {
            //Time.text = "Zeroing";
            Zeroing();
            return;
        }

        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        string r = "";
        //HOURS
        r += ((int)secondsLeft / 3600).ToString() + "h ";
        secondsLeft -= ((int)secondsLeft / 3600) * 3600;
        //MINUTES
        r += ((int)secondsLeft / 60).ToString("00") + "m ";
        //SECONDS
        r += (secondsLeft % 60).ToString("00") + "s";
        Time.text = r;
    }


    public void Zeroing()
    {
        lastTimeClicked = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
        GameManager.S.ZeroingBit();
    }

    private bool Ready()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            //DO SOMETHING WHEN TIMER IS FINISHED
            return true;
        }

        return false;
    }
    
}

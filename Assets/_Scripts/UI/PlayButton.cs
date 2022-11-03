using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject mainMenu;
    public GameObject LeadersUI;
    public void OnClick()
    {
        SpikeGen.S.Play();
        inGameUI.SetActive(true);
        Controll.S.OnPlay();
        mainMenu.SetActive(false);
        MultiTargetCam.S.Play();
    }

    public void OnLeaders()
    {
        LeadersUI.SetActive(true);
    }
}

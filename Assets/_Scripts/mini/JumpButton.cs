using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
{

    //public static bool isPressed = false;
    [SerializeField] private bool jumpIsActive = true;

    //private float timeStart = 0;
    //private float startTime = 0.75f;
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    isPressed = true;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    isPressed = false;
    //}
    //private void Update()
    //{
    //    if (!jumpIsActive)
    //    {
    //        if (timeStart <= 0)
    //        {
    //            jumpIsActive = true;
    //            timeStart = startTime;
    //        }
    //        else { timeStart -= Time.deltaTime; jumpIsActive = false; }

    //        Debug.Log(timeStart);
    //    }
        
    //}

    public void Jump()
    {
        if (Controll.S._grounded)
        {
            Controll.S.MakeJump(3f);
        }

        if (Controll.S.isAirJump) Controll.S.MakeJump(3f);
    }
}

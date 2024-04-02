using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class TimerHandler : MonoBehaviour
{
    private float timer1, timer2, timer3, timer4, timer5, timer6;
    private float minutes1, minutes2, minutes3, minutes4, minutes5, minutes6;
    private float seconds1, seconds2, seconds3, seconds4, seconds5, seconds6;
    public TextMeshProUGUI textField1, textField2, textField3, textField4, textField5, textField6;
    //private bool levelFinished;
    public string objectTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == objectTag)
        {
            if (TeleportManager.index == 0)
            {
                textField1.text = "Level 1 Time : " + minutes1 + "mins, " + seconds1 + "secs";
            }
            else if (TeleportManager.index == 1)
            {
                textField2.text = "Level 2 Time : " + minutes2 + "mins, " + seconds2 + "secs";
            }
            else if (TeleportManager.index == 2)
            {
                textField3.text = "Level 3 Time : " + minutes3 + "mins, " + seconds3 + "secs";
            }
            else if (TeleportManager.index == 3)
            {
                textField4.text = "Level 4 Time : " + minutes4 + "mins, " + seconds4 + "secs";
            }
            else if (TeleportManager.index == 4)
            {
                textField5.text = "Level 5 Time : " + minutes5 + "mins, " + seconds5 + "secs";
            }
            else if (TeleportManager.index == 5)
            {
                textField6.text = "Level 6 Time : " + minutes6 + "mins, " + seconds6 + "secs";
            }
            //Debug.Log("Collision."); 
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((TeleportManager.index == 0) && (!MenuManager.MenuOpen))
        {
            timer1 += Time.deltaTime;
            minutes1 = (int)(timer1 / 60f);
            seconds1 = (int)(timer1 % 60f);
        }
        if ((TeleportManager.index == 1) && (!MenuManager.MenuOpen))
        {
            timer2 += Time.deltaTime;
            minutes2 = (int)(timer2 / 60f);
            seconds2 = (int)(timer2 % 60f);
        }
        if ((TeleportManager.index == 2) && (!MenuManager.MenuOpen))
        {
            timer3 += Time.deltaTime;
            minutes3 = (int)(timer3 / 60f);
            seconds3 = (int)(timer3 % 60f);
        }
        if ((TeleportManager.index == 3) && (!MenuManager.MenuOpen))
        {
            timer4 += Time.deltaTime;
            minutes4 = (int)(timer4 / 60f);
            seconds4 = (int)(timer4 % 60f);
        }
        if ((TeleportManager.index == 4) && (!MenuManager.MenuOpen))
        {
            timer5 += Time.deltaTime;
            minutes5 = (int)(timer5 / 60f);
            seconds5 = (int)(timer5 % 60f);
        }
        if ((TeleportManager.index == 5) && (!MenuManager.MenuOpen))
        {
            timer6 += Time.deltaTime;
            minutes6 = (int)(timer6 / 60f);
            seconds6 = (int)(timer6 % 60f);
        }
    }

    // If using the buttons on the menu to go to previous or next levels, time will reset for respective levels.
    public void ResetTimerOnLevelChange()
    {
        if (TeleportManager.index == 0)
        {
            timer1 = 0;
        }
        if (TeleportManager.index == 1)
        {
            timer2 = 0;
        }
        if (TeleportManager.index == 2)
        {
            timer3 = 0;
        }
        if (TeleportManager.index == 3)
        {
            timer4 = 0;
        }
        if (TeleportManager.index == 4)
        {
            timer5 = 0;
        }
        if (TeleportManager.index == 0)
        {
            timer6 = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class UI : MonoBehaviour
{   
    public TextMeshProUGUI uiDisplay;
    public Slider mainSlider;
    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject lights;
    public string[] text;
    public int size = 32;
    //private bool hidden = false;

    public int index = 0;
    TeleportManager manager;



    //[SerializeField] XRController controller;
    //private InputDevice targetDevice;

    //public GameObject UILayer;

    
    // Start is called before the first frame update
    void Start()
    {   
        manager = GameObject.FindGameObjectWithTag("TeleportManager").GetComponent<TeleportManager>();
        uiDisplay.text = text[index];
        prevButton.SetActive(false);
        testLights();
        testLights();
    }
    

    // Update is called once per frame
    void Update()
    { 
        /*
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

         if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPress ) && menuButtonPress){
            Debug.Log("Button Pressed");
         }
        */ 
    }
    
    public void updateText(){

    }

    public void testNext(){
        if(index != manager.roomNum()-1){
            index ++;
            uiDisplay.text = text[index];
            if(index == manager.roomNum()-1){
                nextButton.SetActive(false);
            }
            if(index == 1){
                prevButton.SetActive(true);
            }
        }
        

        

    }

    public void testPrev(){
        if(index != 0){
            index --;
            uiDisplay.text = text[index];
            if(index == 0){
                prevButton.SetActive(false);
            }
            if(index == manager.roomNum()-2){
                nextButton.SetActive(true);
            }
        }

    }
    /*
    public void toggle(){

        if(hidden){
            UILayer.SetActive(false);
            hidden = false;
        }
        else{
            UILayer.SetActive(true);
            hidden = true;
        }
    }
    */


    public void changeFontSize(){
        size = (int)mainSlider.value;
        uiDisplay.fontSize = size;
    }


    public void testLights(){
        lights.SetActive(!lights.activeSelf);
    }   
}
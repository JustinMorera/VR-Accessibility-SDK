using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class JoystickNav : MonoBehaviour
{
    private bool active;
    private bool firstCall;
    public InputActionProperty Joystick;

    public InputActionProperty button;
    private Vector2 input;
    private bool reset;
    public GameObject uiMan;
    UI ui;
    public GameObject[] Highlights;

    private int min;
    private int max;
    private float time;


    private bool skipPrev;
    private bool skipNext;


    public int selectionIndex;

    public GameObject Tele;
    public GameObject TeleRings;
    public GameObject Waypoints;
    public GameObject MenuM;
    public GameObject XR;
    public GameObject highlight1, highlight2, highlight3, highlight4, highlight5, highlight6, highlight7,highlight8;

    public AudioSource source;

    public AudioClip prevButton;
    public AudioClip resetButton;
    public AudioClip nextButton;
    public AudioClip zoomOutButton;
    public AudioClip zoomInButton;
    public AudioClip contrastButton;
    public AudioClip closeMenu;
    public AudioClip exitButton;

    [SerializeField] XRController controller;
    private UnityEngine.XR.InputDevice targetDevice;

    TeleportManager teleManager;
    TimerHandler timer;
    WayPointHaptics way;
    MenuManager menu;
    DoorHandler door;

    ColorManager colors;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        active = false;
        reset = false;

        if(uiMan.GetComponent<UI>() != null){
            ui = uiMan.GetComponent<UI>();
        }

        if(Tele.GetComponent<TeleportManager>() != null){
           teleManager = Tele.GetComponent<TeleportManager>();
        }

        if(TeleRings.GetComponent<TimerHandler>() != null){
           timer = TeleRings.GetComponent<TimerHandler>();
        }

        if(Waypoints.GetComponent<WayPointHaptics>() != null){
           way = Waypoints.GetComponent<WayPointHaptics>();
        }

        if(MenuM.GetComponent<MenuManager>() != null){
           menu = MenuM.GetComponent<MenuManager>();
        }

        if(XR.GetComponent<DoorHandler>() != null){
           door = XR.GetComponent<DoorHandler>();
        }
        colors = GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>();

        max = Highlights.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {

        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDeviceCharacteristics leftControllerCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        if (active){

            //Initialize
            checkIndex();
            if(skipPrev)
                min = 1;
            else   
                min = 0;

            //Check if this immediately after opening the menu
            if(firstCall){
                selectionIndex = min;
                resetAll();
                Highlights[selectionIndex].SetActive(true);
                //Debug.Log(min + " and " + max);
            }
            
            //If an option is selected
            if(button.action.WasPressedThisFrame()){
                //Execute command.
                execute();
            }


            //Read Joystick position and move selection as required
            input = ReadInput();
            if(input.y > 0.25){
                //Debug.Log("UP");
                setInput(1);
                readButton();
                targetDevice.SendHapticImpulse(0, 0.67f, 0.1f);

            }
            else if(input.y < -0.25){
                //Debug.Log("DOWN");
                setInput(-1);
                readButton();
                targetDevice.SendHapticImpulse(0, 0.67f, 0.1f);
            }
            else{
                //Debug.Log("ZEROED");
                setInput(0);
            }
        }
        else{
            
        }
        
    }

    public Vector2 ReadInput()
        {
            return Joystick.action?.ReadValue<Vector2>() ?? Vector2.zero;
        }

    public void setState(bool value){
        active = value;
        if(value)
            firstCall = true;
    }

    private void setInput(int input){
        //Input is centered, reset
        if(input == 0){
            reset = false;
        }
        //If the input has not been reset, skip
        else if(reset){
            return;
        }
        //Input is up
        else if(input == 1){
            reset = true;
            doUp(); 
        }
        //Input is down
        else if(input == -1){
            reset = true;
            doDown();

        }
        
    }

    private void doUp(){
        //Debug.Log("we go up");
        Highlights[selectionIndex].SetActive(false);
        if(selectionIndex == min){
            selectionIndex = max;
        }
        else{
            selectionIndex --;
        }

        if(selectionIndex == 2 && skipNext)
            selectionIndex --;
        Highlights[selectionIndex].SetActive(true);
        firstCall = false;
        //readButton();
    }

    private void doDown(){
        //Debug.Log("we go down");
        Highlights[selectionIndex].SetActive(false);
        if(selectionIndex == max){
            selectionIndex = min;
        }
        else{
            selectionIndex ++;
        }

        if(selectionIndex == 2 && skipNext)
            selectionIndex ++;

        Highlights[selectionIndex].SetActive(true);
        firstCall = false;
        //readButton();
    }

    private void checkIndex(){
        //At the beginning
        if(ui.index == 0){
            //Disable selection for Prev Button;
            skipPrev = true;
            if(selectionIndex == 0)
                doDown();
            
        }
        else if(ui.index == teleManager.roomNum()-1){
            //Disable selection for Next Buttom
            skipNext = true;
            if(selectionIndex == 2)
                doDown();
            
        }
        else{
            //Enable selection for both buttons
            skipNext = false;
            skipPrev = false;
            
        } 

        
    }

    private void resetAll(){
        foreach (var item in Highlights)
        {
            item.SetActive(false);
        }
    }

    private void execute(){
        switch(selectionIndex){
            case 0:
                ui.testPrev();
                teleManager.previousRoomTP();
                timer.ResetTimerOnLevelChange();
                way.ResetWaypoints();
                break;
            case 1:
                teleManager.resetPosition();
                way.ResetWaypoints();
                door.ResetDoors();
                break; 
            case 2:
                ui.testNext();
                teleManager.nextRoomTP();
                timer.ResetTimerOnLevelChange();
                way.ResetWaypoints();
                break; 
            case 5:
                menu.zoomIn();
                break; 
            case 6:
                menu.zoomOut();
                break; 
            case 7:
                //Color manager
                colors.cycleColors();
                break; 

            case 3:
                menu.quitGame();
                break;

            case 4:
                //Close menu
                menu.closeMenu();
                break;
            default:
                Debug.Log("Somehow the Switch broke");
                break;  
        }
    }

    private void readButton()
    {
        if ((source.isPlaying == false))
        {
            if (highlight1.activeSelf)
            {
                Debug.Log("Previous button.");
                source.PlayOneShot(prevButton);
            }
            else if (highlight2.activeSelf)
            {
                Debug.Log("Reset button.");
                source.PlayOneShot(resetButton);
            }
            else if (highlight3.activeSelf)
            {
                Debug.Log("Next button.");
                source.PlayOneShot(nextButton);
            }
            else if (highlight6.activeSelf)
            {
                Debug.Log("Zoom In button.");
                source.PlayOneShot(zoomInButton);
            }
            else if (highlight7.activeSelf)
            {
                Debug.Log("Zoom Out button.");
                source.PlayOneShot(zoomOutButton);
            }
            else if (highlight8.activeSelf)
            {
                Debug.Log("Contrast button.");
                source.PlayOneShot(contrastButton);
            }

            else if (highlight5.activeSelf)
            {
                //Close
                Debug.Log("Close button.");
                source.PlayOneShot(closeMenu);
            }

             else if (highlight4.activeSelf)
            {   
                //Exit
                Debug.Log("Exit button.");
                source.PlayOneShot(exitButton);
            }
        }
    }
}


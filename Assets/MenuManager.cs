using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class MenuManager : MonoBehaviour
{   
    public Transform head;
    public float SpawnDistance;
    public GameObject menu;
    public GameObject[] OtherUi;
    public bool[] wasActive;
    public InputActionProperty showButton;
    public InputActionProperty display;
    public InputActionProperty hide;
    public GameObject pvManager;
    public GameObject locomotion;
    public GameObject JoyNav;
    PartialVis pv;
    ContinuousMoveProviderBase movement;
    JoystickNav Joy;

    GameObject[] walls;
    GameObject[] inter;
    GameObject[] nonInter;
    GameObject[] obs;

    GameObject[] back;
    

    public static bool MenuOpen;
    public static bool pVOpen;

    private bool JoyLoaded;
    private bool MenuOptionSelected;

    // Start is called before the first frame update
    void Start()
    {   
        //Initialize
        for(int i = 0;i<OtherUi.Length; i++){
            //Set up the wasActive Array
            wasActive[i] = false;
        }

        if(pvManager.GetComponent<PartialVis>() != null){
            pv = pvManager.GetComponent<PartialVis>();
        }

        if(locomotion.GetComponent<ContinuousMoveProviderBase>() != null){
            movement = locomotion.GetComponent<ContinuousMoveProviderBase>();
        }

        
        if(JoyNav.GetComponent<JoystickNav>() != null){
            Joy = JoyNav.GetComponent<JoystickNav>();
            JoyLoaded = true;
        }
        else
            JoyLoaded = false;
            

        pVOpen = false;
        MenuOpen = false;
        MenuOptionSelected = false;
        

        walls = GameObject.FindGameObjectsWithTag("Wall");
        inter = GameObject.FindGameObjectsWithTag("Interactable");
        nonInter = GameObject.FindGameObjectsWithTag("Not Interactable");
        obs = GameObject.FindGameObjectsWithTag("Obstacle");
        back = GameObject.FindGameObjectsWithTag("Background3D");
    }

    // Update is called once per frame
    void Update()
    {
        if (((TeleportManager.index <= 2) || (MenuManager.MenuOpen)))
        {
            for (int i = 0; i < OtherUi.Length; i++)
            {
                OtherUi[i].SetActive(false);
            }
        }

        if(showButton.action.WasPressedThisFrame() || MenuOptionSelected){
            
            //If the menu is closed upon button press
            if(!menu.activeSelf){
                //Check to see what UI elements are currently open, mark them to be re-opened, then hide them
                for(int i = 0;i<OtherUi.Length; i++){
                    if(OtherUi[i].activeSelf){
                        wasActive[i] = true;
                        OtherUi[i].SetActive(false);
                        pv.resetText();
                    }
                    else{
                        wasActive[i] = false;
                    }
                }
                //locomotion.SetActive(false);
                movement.moveSpeed = 0;
                MenuOpen = true;
                if(JoyLoaded){
                    Joy.setState(true);
                    //Debug.Log("Joy Loaded");
                }
                else   
                    Debug.Log("Sumthin Fucked Up");

            
                foreach(GameObject collider in walls){
                        //Debug.Log("Test");
                        collider.layer = LayerMask.NameToLayer("Ignore Raycast");
                        //Debug.Log(collider.name + "has been updated to have the layer of "+ collider.layer);
                }

                foreach(GameObject collider in inter){
                    if(collider.GetComponent<Collider>() != null)
                        collider.layer = LayerMask.NameToLayer("Ignore Raycast");
                }

                foreach(GameObject collider in nonInter){
                    if(collider.GetComponent<Collider>() != null)
                        collider.layer = LayerMask.NameToLayer("Ignore Raycast");
                }

                foreach(GameObject collider in obs){
                    if(collider.GetComponent<Collider>() != null)
                        collider.layer = LayerMask.NameToLayer("Ignore Raycast");
                }

                foreach(GameObject collider in back){
                    if(collider.GetComponent<Collider>() != null)
                        collider.layer = LayerMask.NameToLayer("Ignore Raycast");
                }
                Debug.Log("Menu Open Sequence Completed");
            }
            //If the menu is open
            else{
                //Re-open all menu items that were originally open
                for(int i = 0;i<OtherUi.Length; i++){
                    if(wasActive[i]){
                        OtherUi[i].SetActive(true);
                    }
                }
                //locomotion.SetActive(true);
                movement.moveSpeed = 2;
                MenuOpen = false;
                if(JoyLoaded){
                    Joy.setState(false);
                }

                foreach (GameObject collider in walls){
                    if(collider.GetComponent<Collider>() != null)
                        collider.layer = LayerMask.NameToLayer("Default");
                }

                foreach(GameObject collider in inter){
                    if(collider.GetComponent<Collider>() != null)
                        collider.layer = LayerMask.NameToLayer("Default");
                }

                foreach(GameObject collider in nonInter){
                    if(collider.GetComponent<Collider>() != null)
                        collider.layer = LayerMask.NameToLayer("Default");
                }

                foreach(GameObject collider in obs){
                    if(collider.GetComponent<Collider>() != null)
                        collider.layer = LayerMask.NameToLayer("Default");
                }

                MenuOptionSelected = false;
            }
            menu.SetActive(!menu.activeSelf);
        } else if(display.action.WasPerformedThisFrame() && !menu.activeSelf){
            menu.SetActive(true);
        } else if(hide.action.WasPerformedThisFrame()){
            menu.SetActive(false);
        }

        
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * SpawnDistance;
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
        
    }

    public void zoomIn(){
        if(SpawnDistance >= 1.25){
            SpawnDistance -= (float)0.25;
            Debug.Log("Zoomed in");
        }
    }

    public void zoomOut(){
        if(SpawnDistance <= 3.75){
            SpawnDistance += (float)0.25;
            Debug.Log("Zoomed Out");
        }
    }

    public void quitGame(){
        Application.Quit();
    }

    public void closeMenu(){
        MenuOptionSelected = true;
    }
}

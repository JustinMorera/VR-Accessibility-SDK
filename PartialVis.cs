using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using AccessibilityTags;




public class PartialVis : MonoBehaviour
{   
    public Transform raycastOrigin;
    public InputActionProperty button;

    public TextMeshProUGUI interactable;
    public TextMeshProUGUI details;
    public TextMeshProUGUI obj_name;

    [SerializeField] Color32 trueColor;
    [SerializeField] Color32 falseColor;

    AccessibilityTags.AccessibilityTags tags;
    Object objectInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        trueColor = Color.green;
        falseColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if(button.action.WasPressedThisFrame()){
            Scan();
        }
    }

    

    void Scan(){
        //Raycast send
        RaycastHit hit;
        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
        
        
        //If the raycast hits
        if(Physics.Raycast(ray, out hit, 100)) // Ray hit something
        {
            //Debug.Log(hit.collider.tag);
            //Debug.Log(hit.collider.gameObject);
            //Debug.Log(hit.collider.name);

            //Set the interactable field appropriately
            if(hit.collider.tag == "Interactable"){
                interactable.text = "True";
                interactable.color = trueColor;
            } else{
                interactable.text = "False";
                interactable.color = falseColor;
            }


            //If the object hit has an accessibility script
            if(hit.collider.gameObject.GetComponent<AccessibilityTags.AccessibilityTags>() != null && hit.collider.gameObject.GetComponent<Object>() != null)
            {
                tags = hit.collider.gameObject.GetComponent<AccessibilityTags.AccessibilityTags>();
                objectInfo = hit.collider.gameObject.GetComponent<Object>();
                Debug.Log("This is a " + objectInfo.objectName);

                details.text = tags.AltText + "\n";
                obj_name.text = objectInfo.objectName;
            } 
            else if (hit.collider.gameObject.GetComponent<Object>() != null)
            {
                objectInfo = hit.collider.gameObject.GetComponent<Object>();
                Debug.Log("This is a "+objectInfo.objectName);

                details.text = objectInfo.description + "\n";
                obj_name.text = objectInfo.objectName;
            } 
            else 
            {
                details.text = "None";
                obj_name.text = "None";
            }

            // //If the object hit has an object script
            // if(hit.collider.gameObject.GetComponent<Object>() != null){
            //     objectInfo = hit.collider.gameObject.GetComponent<Object>();
            //     Debug.Log("This is a "+objectInfo.objectName);

            //     details.text = objectInfo.description + "\n";
            //     obj_name.text = objectInfo.objectName;
            // } 
            // else {
            //     details.text = "None";
            //     obj_name.text = "None";
            // }
        }
    }
    public void resetText(){
        interactable.text = "";
        details.text = "";
    }

    public void setColors(Color32 trueC, Color32 falseC){
        trueColor = trueC;
        falseColor = falseC;
        
        return;
    }
    
}


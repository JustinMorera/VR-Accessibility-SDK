using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] XRController controller;
    private InputDevice targetDevice1, targetDevice2;
    public GameObject door4, handle41, handle42, door5, handle51, handle52, door71, door72, handle711, handle712, handle721, handle722, door73, handle731, handle732, player;
    private Vector3 lvl4DoorPos, lvl5DoorPos, lvl71DoorPos, lvl72DoorPos, lvl73DoorPos;
    private Quaternion lvl4DoorRot, lvl5DoorRot, lvl71DoorRot, lvl72DoorRot, lvl73DoorRot;
    public int InterLayer, NonInterLayer, NonLayer;
    private int currLevel;
    private float timer, distanceD4, distanceD5, distanceD71, distanceD72, distanceD73, distanceP;
    private bool doorGrabbed;
    public AudioSource source;
    public AudioClip doorHinge;

    // Start is called before the first frame update
    void Start()
    {
        doorGrabbed = false;

        door4.GetComponent<BoxCollider>().enabled = true;
        door5.GetComponent<BoxCollider>().enabled = true;
        door71.GetComponent<BoxCollider>().enabled = true;
        door72.GetComponent<BoxCollider>().enabled = true;
        door73.GetComponent<BoxCollider>().enabled = true;

        InterLayer = LayerMask.NameToLayer("Interactable");
        NonInterLayer = LayerMask.NameToLayer("Non-Interactable");
        NonLayer = LayerMask.NameToLayer("Ignore Player"); 

        door4.GetComponent<XRGrabInteractable>().enabled = false;
        door5.GetComponent<XRGrabInteractable>().enabled = false;
        door71.GetComponent<XRGrabInteractable>().enabled = false;
        door72.GetComponent<XRGrabInteractable>().enabled = false;
        door73.GetComponent<XRGrabInteractable>().enabled = false;

        currLevel = TeleportManager.index;

        lvl4DoorPos = door4.transform.position;
        lvl4DoorRot = door4.transform.rotation;
        lvl5DoorPos = door5.transform.position;
        lvl5DoorRot = door5.transform.rotation;
        lvl71DoorPos = door71.transform.position;
        lvl71DoorRot = door71.transform.rotation;
        lvl72DoorPos = door72.transform.position;
        lvl72DoorRot = door72.transform.rotation;
        lvl73DoorPos = door73.transform.position;
        lvl73DoorRot = door73.transform.rotation;

        GameObject obj = GameObject.Find("Door");
        source = obj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devicesRight = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devicesRight);

        if (devicesRight.Count > 0)
        {
            targetDevice1 = devicesRight[0];
        }

        List<InputDevice> devicesLeft = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devicesLeft);


        if (devicesLeft.Count > 0)
        {
            targetDevice2 = devicesLeft[0];
        }

        if (currLevel != TeleportManager.index)
        {
            door4.transform.position = lvl4DoorPos;
            door4.transform.rotation = lvl4DoorRot;
            door5.transform.position = lvl5DoorPos;
            door5.transform.rotation = lvl5DoorRot;
            door71.transform.position = lvl71DoorPos;
            door71.transform.rotation = lvl71DoorRot;
            door72.transform.position = lvl72DoorPos;
            door72.transform.rotation = lvl72DoorRot;
            door73.transform.position = lvl73DoorPos;
            door73.transform.rotation = lvl73DoorRot;
            currLevel = TeleportManager.index;
        }
    
        if (TeleportManager.index == 3)
        {
            distanceD4 = Vector3.Distance(player.transform.position, door4.transform.position);
            if (distanceD4 < 3)
            {
                door4.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else
            {
                door4.GetComponent<XRGrabInteractable>().enabled = false;
            }
        }

        if (TeleportManager.index == 4)
        {
            distanceD5 = Vector3.Distance(player.transform.position, door5.transform.position);
            if (distanceD5 < 3)
            {
                door5.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else
            {
                door5.GetComponent<XRGrabInteractable>().enabled = false;
            }
        }

        if (TeleportManager.index == 7)
        {
            distanceD71 = Vector3.Distance(player.transform.position, door71.transform.position);
            distanceD72 = Vector3.Distance(player.transform.position, door72.transform.position);
            distanceD73 = Vector3.Distance(player.transform.position, door73.transform.position);

            if (distanceD71 < 3)
            {
                door71.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else
            {
                door71.GetComponent<XRGrabInteractable>().enabled = false;
            }
            
            if (distanceD72 < 3)
            {
                door72.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else
            {
                door72.GetComponent<XRGrabInteractable>().enabled = false;
            }

            if (distanceD73 < 3)
            {
                door73.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else
            {
                door73.GetComponent<XRGrabInteractable>().enabled = false;
            }
        }

        if (doorGrabbed)
        {
            Debug.Log("Door being grabbed.");
            targetDevice1.SendHapticImpulse(0, 1.0f, 0.1f);
            targetDevice2.SendHapticImpulse(0, 1.0f, 0.1f);
            /*timer += Time.deltaTime;
            if (timer > 0.5) // 0.1)
            {
                Debug.Log("Door being grabbed.");
                targetDevice1.SendHapticImpulse(0, 1.0f, 0.1f);
                targetDevice2.SendHapticImpulse(0, 1.0f, 0.1f);
                timer = 0;
            }*/
        }
    }

    public void disableDoorColliders()
    {
        doorGrabbed = true;
        if (TeleportManager.index == 3)
        {
            Debug.Log("Door in Level 4 grabbed.");
            handle41.layer = NonLayer;
            handle42.layer = NonLayer;
            door4.GetComponent<BoxCollider>().enabled = false;
        }
        else if (TeleportManager.index == 4)
        {
            Debug.Log("Door in Level 5 grabbed.");
            handle51.layer = NonLayer;
            handle52.layer = NonLayer;
            door5.GetComponent<BoxCollider>().enabled = false;
        }
        else if (TeleportManager.index == 7)
        {
            Debug.Log("Door in Office Grabbed");
            handle711.layer = NonLayer;
            handle712.layer = NonLayer;
            handle721.layer = NonLayer;
            handle722.layer = NonLayer;
            handle731.layer = NonLayer;
            handle732.layer = NonLayer;

            door71.GetComponent<BoxCollider>().enabled = false;
            door72.GetComponent<BoxCollider>().enabled = false;
            door73.GetComponent<BoxCollider>().enabled = false;
        }
        source.PlayOneShot(doorHinge);
    }

    public void enableDoorColliders()
    {
        doorGrabbed = false;
        if (TeleportManager.index == 3)
        {
            Debug.Log("Door in Level 4 let go.");
            handle41.layer = InterLayer;
            handle42.layer = InterLayer;
            door4.GetComponent<BoxCollider>().enabled = true;
        }
        else if (TeleportManager.index == 4)
        {
            Debug.Log("Door in Level 5 let go.");
            handle51.layer = InterLayer;
            handle52.layer = InterLayer;
            door5.GetComponent<BoxCollider>().enabled = true;
        }
        else if (TeleportManager.index == 7)
        {
            Debug.Log("Door in Office let go.");
            handle711.layer = InterLayer;
            handle712.layer = InterLayer;
            handle721.layer = InterLayer;
            handle722.layer = InterLayer;
            handle731.layer = InterLayer;
            handle732.layer = InterLayer;

            door71.GetComponent<BoxCollider>().enabled = true;
            door72.GetComponent<BoxCollider>().enabled = true;
            door73.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void ResetDoors()
    {
        door4.transform.position = lvl4DoorPos;
        door4.transform.rotation = lvl4DoorRot;
        door5.transform.position = lvl5DoorPos;
        door5.transform.rotation = lvl5DoorRot;
        door71.transform.position = lvl71DoorPos;
        door71.transform.rotation = lvl71DoorRot;
        door72.transform.position = lvl72DoorPos;
        door72.transform.rotation = lvl72DoorRot;
        door73.transform.position = lvl73DoorPos;
        door73.transform.rotation = lvl73DoorRot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Linq;

public class WayPointHaptics : MonoBehaviour
{
    [SerializeField] XRController controller;
    private InputDevice targetDevice, targetDevice2;
    public Transform raycastOrigin;
    public string objectTag = "Player";
    public GameObject waypoint;
    private bool activeWaypoint;
    private float timer;
    GameObject[] waypoints;
    public AudioSource source;
    public AudioClip waypointReached;

    private void Start()
    {
        // Store all waypoints in array and set all to active.
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].SetActive(true);
        }
        GameObject obj = GameObject.Find("Display Board v2 (Room 1)");
        source = obj.GetComponent<AudioSource>();
        //waypoint.SetActive(true);
    }


    void OnTriggerEnter(Collider other)
    {
        // There is an edge case should the user backtrack and the current waypoint needed to progress is disabled.
        // This can be averted by using a boolean array to keep track, and only send vibrations to the user if the
        // boolean is false.
        if (other.gameObject.tag == objectTag)
        {
            // Deactivate the specific waypoint the user walks into.
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (waypoint.name == waypoints[i].name)
                {
                    //waypoints[i].SetActive(false);
                    if (waypoint.name == "Waypoint 2.1")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 2.2")
                            {
                                Debug.Log("Disabled Waypoint 2.2");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 2.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 2.1")
                            {
                                Debug.Log("Disabled Waypoint 2.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 4.1")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 4.2")
                            {
                                Debug.Log("Disabled Waypoint 4.2");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 4.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 4.1")
                            {
                                Debug.Log("Disabled Waypoint 4.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 5.1")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 5.2")
                            {
                                Debug.Log("Disabled Waypoint 5.2");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 5.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 5.1")
                            {
                                Debug.Log("Disabled Waypoint 5.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 4.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 4.1")
                            {
                                Debug.Log("Disabled Waypoint 4.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 6.1")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 6.2")
                            {
                                Debug.Log("Disabled Waypoint 6.2");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 6.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 6.1")
                            {
                                Debug.Log("Disabled Waypoint 6.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint O.1" || waypoint.name == "Waypoint O.2" || waypoint.name == "Waypoint O.3" || waypoint.name == "Waypoint O.4" || waypoint.name == "Waypoint O.5" || waypoint.name == "Waypoint O.6")
                    {
                        if ((source.isPlaying == false))
                        {
                            source.PlayOneShot(waypointReached);
                        }
                        Debug.Log("Office.");
                        return;
                    }
                    waypoints[i].SetActive(false);

                    if (waypoint.name != "Waypoint 1.2" && waypoint.name != "Waypoint 2.4" && waypoint.name != "Waypoint 3.2" && waypoint.name != "Waypoint 4.4" && waypoint.name != "Waypoint 5.8" && waypoint.name != "Waypoint 6.4" && waypoint.name != "Waypoint D")
                    {
                        // Play Audio Signifying Waypoint Has Been Reached
                        if ((source.isPlaying == false))
                        {
                            source.PlayOneShot(waypointReached);
                        }
                    }
                }
            }
           //waypoint.SetActive(false);
           Debug.Log("Disabling waypoint.");
        }
    }

    private void Update()
    {
        // Quest 2 takes a few seconds to show up in Unity, so have to intialize all of this in Update.
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        List<InputDevice> devicesLeft = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devicesLeft);


        if (devicesLeft.Count > 0)
        {
            targetDevice2 = devicesLeft[0];
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            RaycastHit hit;
            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);

            // If a Raycast of length 2 detectes a hit.
            if (Physics.Raycast(ray, out hit, 50))
            {
                var hitPoint = hit.point;
                hitPoint.y = 0;

                var playerPosition = raycastOrigin.position;
                playerPosition.y = 0;

                float distance = Vector3.Distance(hitPoint, playerPosition);

                if ((hit.transform.tag == "Waypoint") && (waypoint.activeSelf) && ((hit.transform.name != "Door Handle") || (hit.transform.name != "Door Handle")))
                {
                    timer += Time.deltaTime;
                    if (timer > 0.6) // 0.1)
                    {
                        Debug.Log("Detected waypoint.");
                        targetDevice.SendHapticImpulse(0, 1.0f, 0.1f);
                        timer = 0;
                    }
                }
                
                else if ((distance <= 2) && ((hit.transform.name == "Door") || (hit.transform.name == "Door Handle") || (hit.transform.name == "Door Handle")))
                {
                    timer += Time.deltaTime;
                    if (timer > 0.3)
                    {
                        Debug.Log("Door detected.");
                        targetDevice.SendHapticImpulse(0, 1.0f, 0.1f);
                        targetDevice2.SendHapticImpulse(0, 1.0f, 0.1f);
                        timer = 0;
                    }
                }

                else
                {
                    if ((TeleportManager.index == 2 || TeleportManager.index == 3 || TeleportManager.index == 4 || TeleportManager.index == 5 || TeleportManager.index == 6 || TeleportManager.index == 7) && (hit.transform.tag != "Floor") && (!MenuManager.MenuOpen))
                    //if ((((TeleportManager.index >= 2) && (TeleportManager.index < 6)) && (!MenuManager.MenuOpen)) || (TeleportManager.index == 7))
                    {
                        if (distance > 0 && distance <= 0.67)
                        {
                            Debug.Log("Item is between 0 and 0.67 units away.");
                            targetDevice.SendHapticImpulse(0, 1.0f, 0.1f);
                        }
                        else if (distance > 0.67 && distance <= 1.33)
                        {
                            Debug.Log("Item is between 0.67 and 1.33 units away.");
                            targetDevice.SendHapticImpulse(0, 0.67f, 0.1f);
                        }
                        else if (distance > 1.33 && distance < 2.0)
                        {
                            Debug.Log("Item is between 1.33 and 2.0 units away.");
                            targetDevice.SendHapticImpulse(0, 0.33f, 0.1f);
                        }
                    }
                }
            }
        }
    }

    public void ResetWaypoints()
    {
        // Reset all waypoints' active status on level reset / second tries.
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (!waypoints[i].activeSelf)
            {
                waypoints[i].SetActive(true);
            }
            //waypoints[i].SetActive(true);
        }
    }
}

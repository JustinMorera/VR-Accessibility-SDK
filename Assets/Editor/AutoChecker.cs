using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using AccessibilityTags;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// Editor script that Adds AltText to ALL GameObjects
[CustomEditor(typeof(GameObject))]

public class AutoChecker : Editor
{
    private static List<string> duplicateNames = new List<string>();
    private static List<GameObject> duplicateAltText = new List<GameObject>();

    // Right-click option for GameObjects
    [MenuItem("Tools/Check Alt-Text")]
    private static void check(MenuCommand menuCommand)
    {
            // If game object has renderer
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
            Renderer renderer;
            Collider collider;
            

            foreach (GameObject obj in objects)
            {
                if (obj != null)
                {
                    // Store object's AccessibilityTags script, if it exists
                    AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();

                    // Check if object exists and has an active Collider and a Renderer script attached
                    renderer = obj.GetComponent<Renderer>();
                    collider = obj.GetComponent<Collider>();
                    
                    if (collider != null && collider.enabled == true)
                    {
                        if (renderer != null)
                        {
                            // Object name warnings
                            if (!obj.name.Contains(" ") && !obj.name.Contains("_")) // object name is one word
                            {
                                Debug.Log(obj.name + " is one word. While not necessary, it is recommended you make the name more descriptive.");
                            }

                            // Checks for duplicated object names
                            CheckForDuplicateName(obj);
                            CheckForDuplicateAltText(obj, script);

                            // Check if object has AccessibilityTags script
                            if (script != null)
                            {
                                // Check if object has alt text
                                if (script.AltText != null)
                                {
                                    // If alt text is empty
                                    if (script.AltText == "")
                                    {
                                        Debug.LogWarning(obj.name + " needs alt text!");
                                    }
                                    else // If alt text is filled out
                                    {
                                        if (!script.AltText.Contains(" ")) // alt text is only one word
                                        {
                                            Debug.LogWarning("Alt Text is too short for " + obj.name + ". Please add a description.");
                                        }
                                    }

                                    // Check if alt text is sufficient
                                    string firstSentence = "This is a " + obj.name + ". ";
                                    if (script.AltText.Equals(firstSentence))
                                    {
                                        Debug.LogWarning("Alt text is too short for " + obj.name + ". Please add a description.");
                                    }
                                }
                                else // there should be alt text
                                {
                                    // EditorGUILayout.HelpBox("This object needs alt text!", MessageType.Warning);
                                    Debug.LogWarning(obj.name + " needs alt text!");
                                }
                            }
                            else // there should be an AccessibilityTags script
                            {
                                Debug.LogWarning(obj.name + " needs an AccessibilityTags script!");
                            }
                            
                        }
                    }
                    EditorUtility.SetDirty(obj);
                }
            }

            // clear lists
            duplicateNames.Clear();
            duplicateAltText.Clear();
    }

    // check for duplicate alt text and duplicate object names
    private static void CheckForDuplicateAltText(GameObject obj, AccessibilityTags.AccessibilityTags script)
    {
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        Renderer renderer;
        Collider collider;

        foreach (GameObject otherObj in objectsInScene)
        {
            renderer = otherObj.GetComponent<Renderer>();
            collider = otherObj.GetComponent<Collider>();

            if (otherObj != obj && otherObj != null && renderer != null && collider != null && collider.enabled == true && !duplicateAltText.Contains(otherObj) && !duplicateAltText.Contains(obj))
            {
                AccessibilityTags.AccessibilityTags otherScript = otherObj.GetComponent<AccessibilityTags.AccessibilityTags>();
                
                //check for duplicate alt-text
                if (otherScript != null && script.AltText == otherScript.AltText && script.AltText != "" && otherScript.AltText != "")
                {
                    Debug.Log("Duplicate alt text found for objects: " + obj.name + " and " + otherObj.name + ". Please check if these objects should be differentiated more.");
                    duplicateAltText.Add(otherObj);

                    if (!duplicateAltText.Contains(otherObj))
                    {
                        duplicateAltText.Add(otherObj);
                    }
                }

            }
            else
            {
                continue;
            }
        }
    }

    private static void CheckForDuplicateName(GameObject obj)
    {
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        string nameWithoutNumber1 = RemoveNumberAtEnd(obj.name); // to compare if it ends in a number
        string nameWithoutNumber2 = RemoveNumberInParentheses(obj.name); // taking into account numbers in parentheses
        Renderer renderer;
        Collider collider;

        foreach (GameObject otherObj in objectsInScene)
        {
            renderer = otherObj.GetComponent<Renderer>();
            collider = otherObj.GetComponent<Collider>();

            if (otherObj != obj && otherObj != null && renderer != null && collider != null && collider.enabled == true && !duplicateNames.Contains(nameWithoutNumber1) && !duplicateNames.Contains(nameWithoutNumber2))
            {
                // duplicate object names with numbers at the end
                string nameWithoutNumber3 = RemoveNumberAtEnd(otherObj.name); // to compare with obj
                string nameWithoutNumber4 = RemoveNumberInParentheses(otherObj.name); // to compare with obj
                
                // compare 2 objects with numbers at the end of the object names
                bool compareNames1 = String.Equals(nameWithoutNumber1, nameWithoutNumber3, StringComparison.OrdinalIgnoreCase);
                bool compareNames2 = String.Equals(nameWithoutNumber2, nameWithoutNumber4, StringComparison.OrdinalIgnoreCase);

                //check for duplicate object names
                if (obj.name == otherObj.name || compareNames1 == true || compareNames2 == true)
                {
                    Debug.Log("Duplicate name found for object: " + obj.name + ". Please check if these objects should be differentiated more.");
                    duplicateNames.Add(nameWithoutNumber1);
                    duplicateNames.Add(nameWithoutNumber2);
                    continue;
                }
            }
            else
            {
                continue;
            }
        }
    }

    private static string RemoveNumberAtEnd(string name)
    {
        // Removes number at the end of a string to check for duplicate objects
        string pattern = @"\d+$";
        return Regex.Replace(name, pattern, "");
    }

    private static string RemoveNumberInParentheses(string name)
    {
        // Removes number in parentheses at the end of the string to check for duplicate objects
        string pattern = @" \(\d+\)$";
        return Regex.Replace(name, pattern, "");
    }
}
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AutoChecker : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using AccessibilityTags;     
//using static AutoAccessibility;
using System.Text.RegularExpressions;

// Editor script that Adds AltText to ALL GameObjects
[CustomEditor(typeof(GameObject))]

// Emmanuelle
public class AutoChecker : Editor
{
    // Right-click option for GameObjects
    [MenuItem("Tools/Check Alt-Text")]
    private static void check(MenuCommand menuCommand)
    {
        //#if UNITY_EDITOR
            // Draws the built-in inspector
            // DrawDefaultInspector();

            // If game object has renderer
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
            Renderer renderer;
            MeshCollider colliderMesh;
            BoxCollider colliderBox;
            SphereCollider colliderSphere;
            CapsuleCollider colliderCapsule;

            foreach (GameObject obj in objects)
            {
                if (obj != null)
                {
                    // Store object's AccessibilityTags script, if it exists
                    AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();
                    //// If script exists, update altText to object's name
                    //if (script == null)
                    //{
                    //    //script = Undo.AddComponent<AccessibilityTags.AccessibilityTags>(obj);
                    //    //Debug.Log("Alt Text successfully added to " + obj.name);

                    //    AutoAccessibility.AddField();
                    //}

                    // Check if object exists and has an active Collider and a Renderer script attached
                    renderer = obj.GetComponent<Renderer>();
                    colliderMesh = obj.GetComponent<MeshCollider>();
                    colliderBox = obj.GetComponent<BoxCollider>();
                    colliderSphere = obj.GetComponent<SphereCollider>();
                    colliderCapsule = obj.GetComponent<CapsuleCollider>();
                    if ((colliderMesh != null && colliderMesh.enabled == true) || (colliderBox != null && colliderBox.enabled == true) || (colliderSphere != null && colliderSphere.enabled == true) || (colliderCapsule != null && colliderCapsule.enabled == true))
                    {
                        if (renderer != null)
                        {
                            if (script != null)
                            {
                                if (script.AltText != null)
                                {
                                    // If alt text is empty
                                    if (script.AltText == "")
                                    {
                                        // AutoAccessibility.AddField();
                                        // EditorGUILayout.HelpBox("This object needs alt text.", MessageType.Warning);
                                        Debug.LogWarning(obj.name + " needs alt text!");
                                    }
                                    else // If alt text is filled out
                                    {
                                        if (script.AltText.Contains(" ")) // alt text has a description
                                        {
                                            Debug.Log("Alt Text is more than one word for " + obj.name);
                                        }
                                        else // alt text is only one word
                                        {
                                            // EditorGUILayout.HelpBox("Alt text is not descriptive enough.", MessageType.Warning);
                                            Debug.LogWarning("Alt Text is one word for " + obj.name + ".");
                                        }
                                    }
                                }
                                else // there should be alt text
                                {
                                    // EditorGUILayout.HelpBox("This object needs alt text!", MessageType.Warning);
                                    Debug.LogWarning(obj.name + " needs alt text!");
                                }
                            }
                            

                            // Object name warnings
                            if (obj.name.Contains(" ") || obj.name.Contains("_")) // object name is more than one word
                            {
                                Debug.Log("Object Name is more than one word for " + obj.name);
                            }
                            else // object name is one word
                            {
                                // EditorGUILayout.HelpBox("Object Name is one word. While not necessary, it is recommended you make it more descriptive.", MessageType.Info);
                                Debug.LogWarning(obj.name + " is one word. While not necessary, it is recommended you make it more descriptive.");
                            }

                            // Checks for duplicated object names
                            CheckForDuplicateAltText(obj, script);
                        }
                        // else
                        // {
                        //     Debug.Log("no active renderer");
                        // }
                    }
                    // else
                    // {
                    //     Debug.Log("no active collider");
                    // }
                    // Mark selected GameObject as dirty to save changes
                    EditorUtility.SetDirty(obj);
                }
            }
        //#endif
    }

    //private static void checkAltText ()
    //{
    //    // GameObject selectedObject = menuCommand.context as GameObject;
    //    AccessibilityTags.AccessibilityTags script = selectedObject.GetComponent<AccessibilityTags.AccessibilityTags>();

    //    // i hate this method im doing to try to fix this i cant just keep switching back and forth between vs code and visual studio
    //    // just to try to figure out whats going on bc i can only edit on vs code but it doesnt tell me anything while visual studio
    //    // shows me whats actually going on w the code but it wont let me edit

    //    if (script.AltText != null)
    //    {
    //        // If altText is empty
    //        if (script.AltText == "")
    //        {
    //            // run AutoAccessibility.cs to fill it out probably or alert developer
    //            AutoAccessibility.AddField();
    //        }
    //        // If altText is filled out
    //        else
    //        {
    //            public override void OnInspectorGUI()
    //            {
    //                base.OnInspectorGUI();

    //                // Check if altText is a duplicate (same name as another object or ends with a number)
    //                CheckForDuplicateAltText(object, script);
    //                // code here to compare altText of one object to altText of another object (above)

    //                // Check if altText is only one word
    //                if (script.AltText.Contains(" "))
    //                {
    //                    Debug.Log("Alt Text is more than one word for " + object.name);
    //                }
    //                else
    //                {
    //                            // alert developer asking if they are sure about keeping it as one word
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("Alt Text is null for " + object.name);
    //        // alert developer
    //    }
    //}

    // check for duplicate alt text and duplicate object names
    private static void CheckForDuplicateAltText(GameObject obj, AccessibilityTags.AccessibilityTags script)
    {
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        string nameWithoutNumber1 = RemoveNumberAtEnd(obj.name); // for later comparison

        foreach (GameObject otherObj in objectsInScene)
        {
            if (otherObj != obj)
            {
                AccessibilityTags.AccessibilityTags otherScript = otherObj.GetComponent<AccessibilityTags.AccessibilityTags>();
                //check for duplicate alt-text
                if (otherScript != null && script.AltText == otherScript.AltText)
                {
                    //EditorGUILayout.HelpBox("Duplicate alt text found. Please check if these objects should be differentiated more.", MessageType.Info);
                    Debug.LogWarning("Duplicate altText found for objects: " + obj.name + " and " + otherObj.name);
                }

                //check for duplicate object names
                if (obj.name == otherObj.name)
                {
                    //EditorGUILayout.HelpBox("Duplicate Object Names found. Please check if these objects should be differentiated more.", MessageType.Info);
                    Debug.LogWarning("Duplicate name found for objects with name: " + obj.name);
                    continue;
                }

                // duplicate object names with numbers at the end
                string nameWithoutNumber2 = RemoveNumberAtEnd(otherObj.name); // to compare with obj
                // compare 2 objects with numbers at the end of the object names
                bool compareNames = String.Equals(nameWithoutNumber1, nameWithoutNumber2, StringComparison.OrdinalIgnoreCase);

                if (compareNames == true) // these are considered duplicates still
                {
                    //EditorGUILayout.HelpBox("Duplicate Object Names found. Please check if these objects should be differentiated more.", MessageType.Info);
                    Debug.LogWarning("Duplicate name found for objects with name: " + obj.name);
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
}

// ---------------------------------------------

//Kasidy
//public class AutoChecker2 : Editor{

//    // Store GameObjects
//    GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

//    foreach (GameObject obj in objects){
//        //only check objects with the accessibility tad
//        AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();
//        // If script exists, start checking
//        if (script != null){
//            //check if alt text is null or just " "
//            if(script.AltText == null || script.AltText == " "){
//                Debug.LogWarning("Object with name: " + obj.Name + " has no alt-text.");
//            }
                
//            //check for duplicates
//            GameObject[] otherObjects = GameObject.FindObjectsOfType<GameObject>();

//            foreach (GameObject otherObj in otherObjects){
//                if (otherObj != obj){
//                    AccessibilityTags.AccessibilityTags otherScript = otherObj.GetComponent<AccessibilityTags.AccessibilityTags>();
//                    //check for duplicate alt-text
                    
//                    //!!!!!!!!!!
//                    //SHOULD WE ONLY CHECK THE ALT TEXT AND IGNORE THE NAME
//                    //LIKE CHECK THE NAME AND TEXT SEPARATELY. 
//                    //two things with diff names could have the same alt-text and vice versa?
//                    //!!!!!!!!!!

//                    if (otherScript != null && script.AltText == otherScript.AltText){
//                        Debug.LogWarning("Duplicate altText found for objects: " + obj.name + " and " + otherObj.name);
//                    }
//                    //check for duplicate object names
//                    if(obj.name == otherObj.name){
//                        Debug.LogWarning("Duplicate name found for objects with name: " + obj.name);

//                    }
//                }
//            }
                
//            //check if it is short
//            //change number???
//            //alt text format: "This is a " + obj.name + ". " + description + " ";
//            if(script.AltText.length < 30){
//                Debug.LogWarning("the alt-text for object with name: " + obj.Name + " is short. Consider adding more detail.")
//            }
//         }
//    }

        
// }
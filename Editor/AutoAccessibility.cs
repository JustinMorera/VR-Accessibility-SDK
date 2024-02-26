using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using AccessibilityTags;

// Editor script that Adds AltText to ALL GameObjects
[CustomEditor(typeof(GameObject))]
public class AutoAccessibility : Editor
{    
    // Right-click option for GameObjects
    [MenuItem("Tools/Add Accessible Field(s) to entire scene")]
    private static void AddField(MenuCommand menuCommand)
    {
        // Store GameObjects
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        Renderer renderer;
        MeshCollider collider;
        // Iterate through each object
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                // Check if object exists and has a Collider and an active Renderer script attached
                renderer = obj.GetComponent<Renderer>();
                collider = obj.GetComponent<MeshCollider>();
                if (collider != null)
                {
                    if (renderer != null && renderer.enabled == true)
                    {
                        string text = "This is a " + obj.name + ". ";

                        // Check if object has a description somewhere
                        Component[] components = obj.GetComponents<Component>();
                        foreach (Component component in components)
                        {
                            Type type = component.GetType();
                            PropertyInfo descriptionProperty = type.GetProperty("description");

                            if (descriptionProperty != null)
                            {
                                string description = (string)descriptionProperty.GetValue(component, null);
                                Debug.Log("Description found in component: " + type.Name + " - " + description);
                                text += description + " ";
                                break;
                            }
                        }

                        // Store object's AccessibilityTags script, if it exists
                        AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();
                        // If script exists, update altText to object's name
                        if (script != null)
                        {
                            // if (obj.interactable == true)
                            // {
                            //     text += "It is interactible.";
                            // }
                            // else
                            // {
                            //     text += "It is NOT interactible.";
                            // }
                            script.AltText = text;
                            Debug.Log("Alt Text successfully updated for " + obj.name);
                        }
                        else // Otherwise, add accessibility script and altText
                        {
                            script = Undo.AddComponent<AccessibilityTags.AccessibilityTags>(obj);
                            script.AltText = text;
                            Debug.Log("Alt Text successfully added to " + obj.name);
                        }
                        // Mark selected GameObject as dirty to save changes
                        EditorUtility.SetDirty(obj);
                    }
                    else
                    {
                        Debug.Log("Failed to add Alt Text to " + obj.name + " (selected object may not have an active Renderer)");
                    }
                }
                else
                {
                    Debug.Log("Failed to add Alt Text to " + obj.name + " (selected object may not have a Mesh Collider)");
                }
            }
        }
        // Mark scene dirty to save changes to the scene
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}

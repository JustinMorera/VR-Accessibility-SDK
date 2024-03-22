using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;
using System.Reflection;
using AccessibilityTags;

// Editor script that Adds AltText to ALL GameObjects
[CustomEditor(typeof(GameObject))]
public class AutoAccessibility : Editor
{    
    // Right-click option for GameObjects
    [MenuItem("Tools/Add Accessible Field(s) to entire scene")]
    private static void AddFields(MenuCommand menuCommand)
    {
    #if UNITY_EDITOR
        // Store GameObjects
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        Renderer renderer;
        MeshCollider collider;
        // Iterate through each object
        foreach (GameObject obj in objects)
        {
            Debug.Log("Begin " + obj.name);
            if (obj != null)
            {
                Debug.Log("1");
                // Store object's AccessibilityTags script, if it exists
                AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();
                // If script exists, update altText to object's name
                if (script == null)
                {
                    script = Undo.AddComponent<AccessibilityTags.AccessibilityTags>(obj);
                    Debug.Log("Alt Text successfully added to " + obj.name);
                }
                // Check if object exists and has an active Collider and a Renderer script attached
                renderer = obj.GetComponent<Renderer>();
                collider = obj.GetComponent<MeshCollider>();
                if (collider != null && collider.enabled == true)
                {
                    Debug.Log("2");
                    if (renderer != null)
                    {
                        string text = "This is a " + obj.name + ". ";

                        // Check if object has a description somewhere
                        Component[] components = obj.GetComponents<Component>();
                        foreach (Component component in components)
                        {
                            Debug.Log("3: " + component);
                            if (component != null)
                            {
                                Type type = component.GetType();
                                Debug.Log("4");
                                FieldInfo descriptionField = type.GetField("description", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy);

                                if (descriptionField != null)
                                {
                                    string description = (string)descriptionField.GetValue(component);
                                    Debug.Log("Description found in component: " + obj.name + " - " + type.Name);
                                    text += description + " ";
                                    break;
                                }
                            }
                        }
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
                    else
                    {
                        Debug.Log("Failed to add Alt Text to " + obj.name + " (selected object may not have an active Renderer)");
                    }
                }
                else
                {
                    Debug.Log("Failed to add Alt Text to " + obj.name + " (selected object may not have a Mesh Collider)");
                }
                // Mark selected GameObject as dirty to save changes
                EditorUtility.SetDirty(obj);
            }
        }
        // Mark scene dirty to save changes to the scene
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    #endif
    }

    [MenuItem("Tools/Remove Accessible Field(s) from entire scene")]
    private static void RemoveFields(MenuCommand menuCommand)
    {
    #if UNITY_EDITOR
        // Store GameObjects
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        // Iterate through each object
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                // Store object's AccessibilityTags script, if it exists
                AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();
                // If script exists, update altText to object's name
                if (script != null)
                {
                    DestroyImmediate(script);
                    Debug.Log("Accessibility Fields successfully removed from " + obj.name);
                }
            }
        }
        // Mark scene dirty to save changes to the scene
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    #endif
    }
}

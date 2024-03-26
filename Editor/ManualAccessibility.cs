using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using AccessibilityTags;

// Editor script that Adds AltText to a GameObject
[CustomEditor(typeof(GameObject))]
public class ManualAccessibility : Editor
{    
    // Right-click option for GameObjects
    [MenuItem("GameObject/Add Accessible Field(s)")]
    private static void AddField(MenuCommand menuCommand)
    {
        // Store selected GameObject
        GameObject selectedObject = menuCommand.context as GameObject;
        // Check if object exists and has a Mesh Collider script attached (may not be necessary for manual addition of tags by devs)
        if (selectedObject != null)
        {
            string text = "This is a " + selectedObject.name + ". ";

            // Check if object has a description somewhere
            Component[] components = selectedObject.GetComponents<Component>();
            foreach (Component component in components)
            {
                Type type = component.GetType();
                FieldInfo descriptionField = type.GetField("description", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy);

                if (descriptionField != null)
                {
                    string description = (string)descriptionField.GetValue(component);
                    Debug.Log("Description found in component: " + selectedObject.name + " - " + type.Name);
                    text += description + " ";
                    break;
                }
            }

            // Store object's AccessibilityTags script, if it exists
            AccessibilityTags.AccessibilityTags script = selectedObject.GetComponent<AccessibilityTags.AccessibilityTags>();
            // If script exists, update altText to object's name
            if (script != null)
            {
                script.AltText = text;
                Debug.Log("Alt Text successfully updated for " + selectedObject.name);
            }
            else // Otherwise, add altText as object's name
            {
                script = Undo.AddComponent<AccessibilityTags.AccessibilityTags>(selectedObject);
                script.AltText = text;
                Debug.Log("Alt Text successfully added to " + selectedObject.name);
            }
            // Mark selected GameObject as dirty to save changes
            EditorUtility.SetDirty(selectedObject);
            // Mark scene dirty to save changes to the scene
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}

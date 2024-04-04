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
        Rigidbody rigidbody;
        // Store selected GameObject
        GameObject selectedObject = menuCommand.context as GameObject;
        // Check if object exists
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
            if (script == null)
            {
                script = Undo.AddComponent<AccessibilityTags.AccessibilityTags>(selectedObject);
            }
            script.AltText = text;
            Debug.Log("Alt Text successfully added to " + selectedObject.name);

            rigidbody = obj.GetComponent<Rigidbody>();
            // Check if object has a Rigidbody script attached for interactibility
            if (rigidbody != null && rigidbody.isKinematic == false) // If isKinematic is false, object can be picked up/manipulated
            {
                script.Interactable = true;
            }
            else
            {
                script.Interactable = false;
            }
        
            // Mark selected GameObject as dirty to save changes
            EditorUtility.SetDirty(selectedObject);
            // Mark scene dirty to save changes to the scene
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}

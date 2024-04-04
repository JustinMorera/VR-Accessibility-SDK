using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccessibilityTags
{
    public class AccessibilityTags : MonoBehaviour
    {
        [SerializeField]
        private string altText;
        private bool interactable

        public string AltText
        {
            get{ return altText; }
            set{ altText = value; }
        }

        public bool Interactable
        {
            get{ return interactable; }
            set{ interactable = value; }
        }
    }
}
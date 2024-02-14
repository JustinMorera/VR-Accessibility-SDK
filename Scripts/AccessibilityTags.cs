using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccessibilityTags
{
    public class AccessibilityTags : MonoBehaviour
    {
        [SerializeField]
        private string altText;

        public string AltText
        {
            get{ return altText; }
            set{ altText = value; }
        }
    }
}
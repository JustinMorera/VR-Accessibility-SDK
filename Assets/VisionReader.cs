using Meta.WitAi.TTS.Utilities;
using UnityEngine;
using AccessibilityTags;

namespace Assets
{
    public class VisionReader
    {
        private bool speakerEnabled = true;

        private TTSSpeaker ttsSpeaker;

        private bool _isSpeaking = false;

        /// <summary>
        /// Constructor for VisionReader
        /// </summary>
        /// <param name="speaker"></param>
        public VisionReader(TTSSpeaker speaker)
        {
            ttsSpeaker = speaker;

            // Debug log for speaker events
            ttsSpeaker.Events.OnLoadBegin.AddListener((_ttsSpeaker, s) => Debug.Log($"Loading: {s}"));
            ttsSpeaker.Events.OnLoadSuccess.AddListener((_ttsSpeaker, s) => Debug.Log($"Load success"));
            ttsSpeaker.Events.OnPlaybackStart.AddListener((_ttsSpeaker, s) =>
            {
                _isSpeaking = true;
                Debug.Log($"Speaking: {s}");
            });
            ttsSpeaker.Events.OnPlaybackComplete.AddListener((_ttsSpeaker, s) =>
            {
                _isSpeaking = false;
                Debug.Log($"Playback Complete");
            });
            ttsSpeaker.Events.OnPlaybackCancelled.AddListener((_ttsSpeaker, _data, _s) =>
            {
                _isSpeaking = false;
                Debug.Log($"Playback Cancelled");
            });
        }

        /// <summary>
        /// Speak the name and description of the object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="interactable"></param>
        public void StartReading(string altText)
        {
            if (string.IsNullOrEmpty(altText)) return;

            if (!speakerEnabled) return;
            
            StopReading();

            ttsSpeaker.Speak(altText);
        }

        public void StopReading()
        {
            if (_isSpeaking) ttsSpeaker.Stop();
        }

        /// <summary>
        /// Set the speaker to enabled or disabled
        /// </summary>
        /// <param name="enabled"></param>
        public void SetSpeakerEnabled(bool enabled)
        {
            speakerEnabled = enabled;
        }

        /// <summary>
        /// Check if the speaker is enabled
        /// </summary>
        /// <returns></returns>
        public bool IsSpeakerEnabled()
        {
            return speakerEnabled;
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GlobalComponents
{
    public class SliderDesignStates : MonoBehaviour
    {
        public event Action<float> OnSliderValueChanged;

        private Slider slider => GetComponent<Slider>();


        private float[] states;
        private string sliderPrefKey;


        /// <summary>
        /// Activates the script of slider and sets the values.
        /// </summary>
        /// <param name="assignedStates"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue"></param>
        public float GetSliderComponent(float[] assignedStates, string keyName, float defaultValue)
        {
            states = assignedStates;
            sliderPrefKey = keyName;

            if (PlayerPrefs.HasKey(keyName))
            {
                slider.value = PlayerPrefs.GetFloat(keyName);
            }
            else
            {
                slider.value = defaultValue;
                PlayerPrefs.SetFloat(sliderPrefKey, defaultValue);
            }
            
            slider.onValueChanged.AddListener(OnValueChanged);

            return slider.value;
        }

        /// <summary>
        /// On slider value changed.
        /// </summary>
        /// <param name="value"></param>
        private void OnValueChanged(float value)
        {
            float closestValue = GetClosestState(value);
            
            slider.value = closestValue; 
            
            OnSliderValueChanged?.Invoke(closestValue);

            PlayerPrefs.SetFloat(sliderPrefKey, closestValue);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Get the closest state.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private float GetClosestState(float value)
        {
            float closest = states[0];
            float minDifference = Mathf.Abs(value - closest);

            foreach (float state in states)
            {
                float difference = Mathf.Abs(value - state);
                
                if (difference < minDifference)
                {
                    closest = state;
                    minDifference = difference;
                }
            }

            return closest;
        }
    }
}


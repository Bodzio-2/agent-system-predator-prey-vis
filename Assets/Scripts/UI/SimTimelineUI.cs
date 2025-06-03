using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class SimTimelineUI : MonoBehaviour
    {
        [SerializeField]
        Slider timelineSlider;
        [SerializeField]
        Button playPauseButton;

        [SerializeField]
        TextMeshProUGUI speedDisplay;

        float speed = 1.0f;
        [SerializeField]
        float speedDelta = 0.25f;
        [SerializeField]
        float minimumSpeed = 0.25f;
        [SerializeField]
        float maximumSpeed = 3f;



        public void InitializeTimeline(int length, int curIndex)
        {
            timelineSlider.maxValue = length;
            timelineSlider.value = curIndex;
        }

        public void PlayPause()
        {
            SimulationHandler.Instance.PlayPauseSim();
        }


        public void SetIndex(int timelineIndex)
        {
            timelineSlider.value = timelineIndex;
        }

        public void ManualIndex()
        {
            SimulationHandler.Instance.SelectTimestamp(((int)timelineSlider.value));
        }

        public void IncreaseSpeed()
        {
            speed = Mathf.Clamp(speed + speedDelta, minimumSpeed, maximumSpeed);
            SimulationHandler.Instance.SetSimSpeed(speed);
            if(speedDisplay != null)
                speedDisplay.text = speed.ToString();
        }

        public void DecreaseSpeed()
        {
            speed = Mathf.Clamp(speed - speedDelta, minimumSpeed, maximumSpeed);
            SimulationHandler.Instance.SetSimSpeed(speed);
            if (speedDisplay != null)
                speedDisplay.text = speed.ToString();
        }
    }
}

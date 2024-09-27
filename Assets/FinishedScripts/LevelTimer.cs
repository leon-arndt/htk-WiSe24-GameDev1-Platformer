using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        var levelTime = TimeSpan.FromSeconds( Time.timeSinceLevelLoad);
        timerText.text = $"{levelTime.Minutes}:{levelTime.Seconds:00}.{levelTime.Milliseconds/10:00}";
    }
}

using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    private float time;
    public bool timerActive;

    [SerializeField] TextMeshProUGUI units;
    private int enemiesLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            time += Time.deltaTime;
        }

        TimeSpan displayedTime = TimeSpan.FromSeconds(time);

        timer.text = displayedTime.Minutes.ToString() + displayedTime.Seconds.ToString();


        enemiesLeft = GlobalsVariable.AliveUnitsTeamA.Count;
        units.text = "Units Left: " + enemiesLeft.ToString();
    }
}

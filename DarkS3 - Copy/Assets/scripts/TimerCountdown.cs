using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCountdown : MonoBehaviour
{
    public float timeRemaining = 60;
    public bool timerIsRunning = true;
    public TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0 && timerIsRunning){
            GetComponent<NavigationController>().GoToGameOverScene();
            timeRemaining = 0;
        }
        timeText.text = "Time: " + Mathf.FloorToInt(timeRemaining).ToString();
    }
}

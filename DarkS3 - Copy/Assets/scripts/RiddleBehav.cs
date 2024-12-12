using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class RiddleBehav : MonoBehaviour
{
    float timeLeft = 10.0f;
    bool isSolved = false;
    bool isTriggered = false;
    public TextMeshProUGUI timerText;
    public GameObject panel;
    Rigidbody2D playerRb;
    private string input;
    public GameObject chest;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered){
            timeLeft -= Time.deltaTime;
            if(timeLeft < 0&&!isSolved){
                GetComponent<NavigationController>().GoToGameOverScene();
            }
        }
        timerText.text = ((int) timeLeft).ToString();
    }
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.tag == "Player")
    {
        timerText.gameObject.SetActive(true);
        panel.SetActive(true);
        playerRb = other.GetComponent<Rigidbody2D>();
        isTriggered = true;
        playerRb.velocity = Vector2.zero;
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
    public void CheckAnswer(string answer){
        input = answer;
        if(input=="tree"){
            isSolved=true;
            timerText.gameObject.SetActive(false);
            panel.SetActive(false);
            chest.GetComponent<Animator>().enabled = true;
            playerRb.constraints = RigidbodyConstraints2D.None;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Destroy(this.gameObject);
        }
    }
}

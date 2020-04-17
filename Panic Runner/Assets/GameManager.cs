using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }
    public bool isGameStarted = false;
    public PlayerMotor motor; //keep track of the player

    //Ui and the UI fields
    public Text scoreText, coinText, modieferText;  //modifier is used to speed game 
    private float score, coinScore, modifierScore;


    public void awake()
    {
        Instance = this;
        UpdateScore();
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    }

    public void Update()
    {
        if(MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
        }
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
        modieferText.text = modifierScore.ToString();
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    private float startSpeed = 5f;

    public Transform startingPoint;
    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;

        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }   

    // Update is called once per frame
    void Update()
    {
        
        // score play speed
        //if (!playerControllerScript.gameOver)
        //{
        //    if (playerControllerScript.doubleSpeed)
        //    {
        //        score += 2;
        //    }
        //    else
        //    {
        //        score++;
        //    }
        //    Debug.Log("Score: " + score);
        //}
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * startSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);
        
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * startSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            
            yield return null;
        }

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.gameOver = false;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;

        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        //isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

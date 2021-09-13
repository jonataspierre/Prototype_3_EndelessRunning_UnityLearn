using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private GameManager gameManager;

    public int pointValue;

    private float speed = 25f;
    private float leftBound = -15f;
    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.doubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }            
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            gameManager.UpdateScore(pointValue);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource playerAudio;

    public float jumpForce = 10f;
    public float gravityModifier;
    private float upBound = 10;
    public bool canDoubleJump;
    public bool isOnGround = true;
    public bool gameOver;
    public bool doubleSpeed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the space button was pressed to jump
        if (Input.GetButtonDown("Jump"))
        {   
            if (isOnGround && !gameOver)
            {
                isOnGround = false;
                canDoubleJump = true;
                Jump();
            }
            else if (Input.GetButtonDown("Jump") && canDoubleJump && !gameOver)
            {
                canDoubleJump = false;
                Jump();
            }            
        }

        // Constrains the player on the up bound
        if (transform.position.y > upBound)
        {
            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(Vector3.zero);
        }

        // Verify if the horizontal arrow buttons was pressed for dash speed
        if (Input.GetButton("Horizontal") && !gameOver)
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
            playerAnim.SetBool("Jump_b", false);
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            //Debug.Log("Game Over");            
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1f);
        }
    }

    void Jump()
    {
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);        
        playerAnim.SetTrigger("Jump_trig");
        playerAnim.SetBool("Jump_b", true);
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, 1f);
    }
}

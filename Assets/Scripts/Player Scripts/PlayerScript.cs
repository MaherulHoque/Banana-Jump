using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerBody;

    public float moveSpeed = 2f;
    public float normalPush = 10f;
    public float extraPush = 14f;

    private bool initialPush = false;
    private int pushCount;
    private bool playerDied = false;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (playerDied)
            return;

        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            playerBody.velocity = new Vector2(-moveSpeed, playerBody.velocity.y);
        }

    } // player movement

    void OnTriggerEnter2D(Collider2D target)
    {
        if (playerDied)
            return;

        if (target.tag == "ExtraPush")
        {
            if (!initialPush)
            {
                initialPush = true;

                playerBody.velocity = new Vector2(playerBody.velocity.x, 18f);

                target.gameObject.SetActive(false);

                SoundManager.instance.JumpSoundFX();

                // Exit on trigger enter because of initial push
                return;

            }
        }

        if(target.tag == "NormalPush")
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, normalPush);

            target.gameObject.SetActive(false);

            pushCount++;

            SoundManager.instance.JumpSoundFX();

        }

        if(target.tag == "ExtraPush")
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, extraPush);

            target.gameObject.SetActive(false);

            pushCount++;

            SoundManager.instance.JumpSoundFX();

        }

        if(pushCount == 2)
        {
            pushCount = 0;
            PlatformSpawner.instance.SpawnPlatforms();
        }

        if(target.tag == "FallDown" || target.tag == "Bird")
        {
            playerDied = true;

            SoundManager.instance.GameOverSoundFX();

            GameManager.instance.RestartGame();
        
        }

    } // On trigger enter
    
} // Class

































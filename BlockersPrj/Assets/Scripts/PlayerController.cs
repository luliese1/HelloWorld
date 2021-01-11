using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;

    public float moveSpeed;
    private float moveSpeedStore;

    public float speedMultiplier;

    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;

    private float speedMilestoneCounter;
    private float speedMilestoneCounterStore;

    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool CanDoubleJump;
    private bool stoppedJumping;

    public bool IsGround;
    public LayerMask WhatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    private Collider2D _collider2D;

    private Animator Animator;

    public GameManager gameManager;

    public bool booster;

    public AudioSource JumpSound;
    public AudioSource DeathSound;
    

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        moveSpeedStore = moveSpeed;
        speedMilestoneCounterStore = speedMilestoneCounter;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

    }


    // Update is called once per frame
    void Update()
    {

        //IsGround = Physics2D.IsTouchingLayers(collider2D, WhatIsGround);

        IsGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        if (booster)
        {
            
            Rigidbody2D.velocity = new Vector2(2 * moveSpeed, Rigidbody2D.velocity.y);
        }
        else
        {
            Rigidbody2D.velocity = new Vector2(moveSpeed, Rigidbody2D.velocity.y);
        }


        if (transform.position.x > speedMilestoneCounter)
        {
            speedMilestoneCounter += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

            moveSpeed = moveSpeed * speedMultiplier;
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (IsGround)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
                stoppedJumping = false;

                JumpSound.Play();
            }     

            if(!IsGround && CanDoubleJump)
            {

                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);

                jumpTimeCounter = jumpTime;
                CanDoubleJump = false;
                stoppedJumping = false;

                JumpSound.Play();
            }

        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if(jumpTimeCounter > 0)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (IsGround)
        {
            jumpTimeCounter = jumpTime;
            CanDoubleJump = true;
        }

        Animator.SetFloat("Speed", Rigidbody2D.velocity.x);
        Animator.SetBool("IsGround", IsGround);
        Animator.SetFloat("Velocity", Rigidbody2D.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            DeathSound.Play();
            gameManager.RestartGame();

            moveSpeed = moveSpeedStore;
            speedMilestoneCounter = speedMilestoneCounterStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            
        }
    }




}

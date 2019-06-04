using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

public class Player : MonoBehaviour
{
    public float gravity = -10f;
    public float moveSpeed = 2.5f;
    public float jumpheight = 2.5f;
    public float centreRadius = 0.1f;

    private SpriteRenderer rend;
    private Animator anim;
    private CharacterController2D controller;
    private Vector3 velocity;
    private bool isClimbing = false;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, centreRadius);
    }

    void Update()
    {
        //gets left and right axis assigned in input manager
        float inputH = Input.GetAxis("Horizontal");
        //gets up and down axis of assigned input manager
        float inputV = Input.GetAxis("Vertical");

        //if controller is not grounded
        if (!controller.isGrounded && !isClimbing)
        {
            // Apply delta to gravity
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            // Get Spacebar input
            bool isJumping = Input.GetButtonDown("Jump");
            // If Player pressed jump
            if (isJumping)
            {
                // Make the controller jump
                Jump();
            }
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("jumpY", velocity.y);

        //plays run void
        Move(inputH);
        Climb(inputH, inputV);
        //applies velocity to controller (to get it to move)
        if (!isClimbing)
        {
            controller.move(velocity * Time.deltaTime);
        }
    }

    void Move(float inputH)
    {
        //move player left and right
        velocity.x = inputH * moveSpeed;
        //plays runnging animation
        anim.SetBool("isRunning", inputH != 0);
        //if input doesnt = 0 flip the sprite to face left
        if (inputH != 0)
        {
            //flip sprite to face left
            rend.flipX = inputH < 0;
        }
    }

    private void Climb(float inputH, float inputV)
    {

        bool isOverLadder = false;
        //get list of all hits objects overlapping ladde
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, centreRadius);
        //loop through each point
        foreach (var hit in hits)
        {
            //if the point overlaps a climbable object
            if (hit.tag == "Ladder")
            {
                //player is overlapping ladder
                isOverLadder = true;
                break;
            }
        }

        if (isOverLadder && inputV != 0)
        {
            isClimbing = true;
            velocity.y = 0;
        }

        if (!isOverLadder)
        {
            isClimbing = false;
        }
        //is climbing
        // if is climbing
        //perform logic for climbing

        if (isClimbing)
        {
            Vector3 inputDir = new Vector3(inputH, inputV);
            transform.Translate(inputDir *  moveSpeed * Time.deltaTime);
        }

        anim.SetBool("isClimbing", isClimbing);
        anim.SetFloat("climbSpeed", inputV);
    }

    void Jump()
    {
        //sets velocity on the y axis to jump height
        velocity.y = jumpheight;
        anim.SetTrigger("jump");
    }

    public void Hurt()
    {

    }
}

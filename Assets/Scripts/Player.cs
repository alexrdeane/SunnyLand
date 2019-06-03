﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

public class Player : MonoBehaviour
{
    public float gravity = -10f;
    public float moveSpeed = 2.5f;
    public float jumpheight = 2.5f;

    private SpriteRenderer rend;
    private Animator anim;
    private CharacterController2D controller;
    private Vector3 velocity;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //gets left and right axis assigned in input manager
        float inputH = Input.GetAxis("Horizontal");
        //gets up and down axis of assigned input manager
        float inputV = Input.GetAxis("Vertical");

        //if controller is not grounded
        if (!controller.isGrounded)
        {
            //apply velocity timesed by gravity
            velocity.y += gravity * Time.deltaTime;
        }
        //get spacebar input
        bool isJumping = Input.GetButtonDown("Jump");
        //makes controller jump
        if (isJumping)
        {
            //makes the controller jump
            Jump();
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("jumpY", velocity.y);

        //plays run void
        Run(inputH);
        Climb(inputV);
        //applies velocity to controller (to get it to move)
        controller.move(velocity * Time.deltaTime);
    }

    private void Run(float inputH)
    {
        //move player left and right
        controller.move(transform.right * inputH * (moveSpeed * Time.deltaTime));
        //plays runnging animation
        anim.SetBool("isRunning", inputH != 0);
        //if input doesnt = 0 flip the sprite to face left
        if (inputH != 0)
        {
            //flip sprite to face left
            rend.flipX = inputH < 0;
        }
    }

    private void Climb(float inputV)
    {

    }

    void Jump()
    {
        //sets velocity on the y axis to jump height
        velocity.y = jumpheight;
    }
}

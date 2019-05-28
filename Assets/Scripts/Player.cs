using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

public class Player : MonoBehaviour
{
    public float gravity = -10f;
    private CharacterController2D controller;
    public float moveSpeed = 10f;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputW = Input.GetAxis("Vertical");
        Move(inputH, inputW);
    }

    private void Move(float inputH, float inputW)
    {
        controller.move(transform.right * inputH * (moveSpeed * Time.deltaTime));
        bool isRunning = inputH != 0;
        anim.SetBool("isRunning", true);
    }
}

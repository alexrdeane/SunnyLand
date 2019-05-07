using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
public float gravity = -10f;
private CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
controller = GetComponent(typeof(CharacterController));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

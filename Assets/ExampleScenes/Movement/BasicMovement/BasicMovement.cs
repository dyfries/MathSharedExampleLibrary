using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Vector2 input;

    public float movementSpeed = 1f;


    // Update is called once per frame
    void Update()
    {
        // Capture the input in a Vector2
        input = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // Apply a movement offset to the transform. 
        // movement speed can be used to adjust the speed 
        // Time.deltaTime normalizes the speed across frame rate fluctuations. 
        transform.Translate(input * movementSpeed * Time.deltaTime);
    
    }
}

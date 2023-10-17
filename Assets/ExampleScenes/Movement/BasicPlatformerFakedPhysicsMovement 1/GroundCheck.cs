using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Transform gChecker;
    public float checkRadius = 5;
    public bool IsGrounded = false;
    public LayerMask groundLayer;

    void Start()
    {
        if(gChecker == null)
        {
            Debug.LogError("Ground Check transform is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics.Raycast(gChecker.position, Vector3.down, checkRadius, groundLayer);
    }
}

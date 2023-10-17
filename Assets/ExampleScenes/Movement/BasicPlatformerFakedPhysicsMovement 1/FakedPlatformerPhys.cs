using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * Coded by Marissa and Sujal
*/
public class FakedPlatformerPhys : MonoBehaviour
{
    public Vector2 input;

    public float accelerationRate = 1f;

    private Vector2 currentVelocity;
    private Vector2 currentAcceleration;

    public float maxAcceleration = 1f;
    public float maxVelocity = 10f;
    public float maxDrag = 0.1f;

    public float jumpPower = 1;

    [Header("Gravity")]
    public bool hasGravity = true;
    public float gravityScale = 1f;
    public float mass = 1;
    private float timeInAir = 0;

    [Header("Ground Check")]
    public Transform gChecker;
    public float checkRadius = 5;
    public LayerMask groundLayer;

    void Start()
    {
        if (gChecker == null)
        {
            Debug.LogError("Ground Check transform is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        currentAcceleration = input * accelerationRate;
        // Cap it at max
        // find lesser of max and current acceleration magnitude (directionless speed)
        float currentMaxAccel = Mathf.Min(currentAcceleration.magnitude, maxAcceleration);
        currentAcceleration = currentAcceleration.normalized * currentMaxAccel;

        currentVelocity += currentAcceleration * Time.deltaTime;
        float currentMaxVelocity = Mathf.Min(currentVelocity.magnitude, maxVelocity);

        // Make the velocity round to 0 after reaching threshhold
        if(Mathf.Abs(currentMaxVelocity) < 0.001f)
        {
            currentMaxVelocity = 0;
        }

        // dampen the velocity by drag every frame
        currentVelocity = currentVelocity.normalized * (currentMaxVelocity * maxDrag);

        if (!IsGrounded())
        {
            timeInAir += Time.deltaTime * mass;
            Vector2 gravity = Vector2.down * gravityScale;

            currentVelocity += gravity * (mass + timeInAir);
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                currentVelocity += Vector2.up * jumpPower;
            }
            else
            {
                timeInAir = 0;
                currentVelocity.y = 0;
            }
        }

        Debug.DrawRay(transform.position, currentVelocity, Color.yellow);
        Debug.DrawRay(transform.position, currentAcceleration, Color.red);

        // Do this here so our current Vel on record is in M/s
        transform.Translate(currentVelocity * Time.deltaTime);

    }

    bool IsGrounded()
    {
        if (Physics2D.Raycast(gChecker.position, Vector2.down, checkRadius, groundLayer))
        {
            Debug.DrawRay(gChecker.position, Vector3.down * checkRadius, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(gChecker.position, Vector3.down * checkRadius, Color.red);
            return false;
        }

        
    }
}

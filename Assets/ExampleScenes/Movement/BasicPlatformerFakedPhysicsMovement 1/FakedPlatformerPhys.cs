using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float drag = 0.1f;

    public float jumpPower = 1;
    private bool isJumping = false;

    [Header("Gravity")]
    public bool hasGravity = true;
    public float gravityScale = 1f;
    public float mass = 1;
    private float timeInAir = 0;
    private Vector2 Gravity => Vector2.down * gravityScale;

    [Header("Ground Check")]
    public Transform gChecker;
    public float checkRadius = 5;
    public LayerMask groundLayer;
    private bool isGrounded = false;

    void Start()
    {
        if (!gChecker)
        {
            Debug.LogError("Ground Check transform is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update inputs and ground state
        CacheInputs();
        isGrounded = IsOnGround();

        // Update Movement Values
        UpdateAccel();
        UpdateVelocity();

        if (!isGrounded)
        {
            // Apply gravity
            timeInAir += Time.deltaTime * mass;
            currentVelocity += Gravity * (mass + timeInAir);
        }
        else
        {
            // Check if the jump button is down
            if (isJumping)
            {
                // Jump via increasing velocity
                currentVelocity += Vector2.up * jumpPower;
            }
            else
            {
                // Reset gravity so that we don't go through the floor
                timeInAir = 0;
                currentVelocity.y = 0;
            }
        }

        Debug.DrawRay(transform.position, currentVelocity, Color.yellow);
        Debug.DrawRay(transform.position, currentAcceleration, Color.red);

        // Do this here so our current Vel on record is in M/s
        transform.Translate(currentVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Update input values for Horizontal & Vertical input, and Jump
    /// </summary>
    void CacheInputs()
    {
        // Movement
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Jump
        isJumping = Input.GetButtonDown("Jump");
    }

    /// <summary>
    /// Update Acceleration related values
    /// </summary>
    void UpdateAccel()
    {
        currentAcceleration = input * accelerationRate;
        // Cap it at max
        // find lesser of max and current acceleration magnitude (directionless speed)
        float currentMaxAccel = Mathf.Min(currentAcceleration.magnitude, maxAcceleration);
        currentAcceleration = currentAcceleration.normalized * currentMaxAccel;
    }

    /// <summary>
    /// Update Velocity related values
    /// </summary>
    void UpdateVelocity()
    {
        currentVelocity += currentAcceleration * Time.deltaTime;
        float currentMaxVelocity = Mathf.Min(currentVelocity.magnitude, maxVelocity);

        // Make the velocity round to 0 after reaching threshhold
        if (Mathf.Abs(currentMaxVelocity) < 0.001f) currentMaxVelocity = 0;

        // dampen the velocity by drag every frame
        currentVelocity = currentVelocity.normalized * (currentMaxVelocity * drag);
    }

    /// <summary>
    /// Check if the player is grounded via the check radius
    /// </summary>
    /// <returns>If the player is grounded</returns>
    bool IsOnGround()
    {
        if (!gChecker) return false; // Null Check
        return Physics2D.Raycast(gChecker.position, Vector2.down, checkRadius, groundLayer);
    }
}

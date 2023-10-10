using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakedPhysicsExample : MonoBehaviour
{
    public Vector2 input;

    public float accelerationRate = 1f;

    public Vector2 currentVelocity;
    public Vector2 currentAcceleration;

    public float maxAcceleration = 1f;
    public float maxVelocity = 10f;

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
        currentVelocity = currentVelocity.normalized * currentMaxVelocity;

        Debug.DrawRay(transform.position, currentVelocity, Color.yellow);
        Debug.DrawRay(transform.position, currentAcceleration, Color.red);

        // Do this here so our current Vel on record is in M/s
        transform.Translate(currentVelocity * Time.deltaTime);

    }
}

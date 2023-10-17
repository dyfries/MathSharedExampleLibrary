using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create by Sean Piche and Derek Baert 10/17/2023
/// </summary>
public enum RotationModeExample { RotateTowardsTarget, SnapToAngle, QuaternionMultiplication, AngleAxis, QuaternionSetFromToRotation }
public class RotationExample : MonoBehaviour
{
    [SerializeField]
    private RotationModeExample rotationMode = RotationModeExample.RotateTowardsTarget;
    //Dont try to assign to a quaternion directly
    [SerializeField]
    private Vector3 targetRotation;


    [SerializeField]
    private GameObject target;

    private Quaternion startRotation;

    private float timeFromStart;

    /// <summary>
    /// Sets the initial startRotation
    /// </summary>
    void Start()
    {
        startRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationMode == RotationModeExample.RotateTowardsTarget)
        {
            RotateTowardsTarget(Time.deltaTime);
        }
        else if (rotationMode == RotationModeExample.SnapToAngle)
        {
            SnapToRotation();
        }
        else if (rotationMode == RotationModeExample.QuaternionMultiplication)
        {
            QuaternionMultiplication();
        }
        else if (rotationMode == RotationModeExample.AngleAxis) 
        {
            AngleAxis();
        }
        else if (rotationMode == RotationModeExample.QuaternionSetFromToRotation) 
        {
            
        }
    }

    /// <summary>
    /// Rotates overtime to the target transform
    /// </summary>
    /// <param name="delta">Time.DeltaTime</param>
    private void RotateTowardsTarget(float delta)
    {
        if(targetRotation != this.transform.rotation.eulerAngles)
        {
            timeFromStart += delta;
            Mathf.Clamp(timeFromStart, 0, 1);
            transform.rotation = Quaternion.Slerp(startRotation, Quaternion.Euler(targetRotation), timeFromStart);
            if (timeFromStart > 1)
            {
                startRotation = this.transform.rotation;
                timeFromStart = 0;
            }
        }
    }

    /// <summary>
    /// Snaps to the target rotation angle
    /// </summary>
    private void SnapToRotation()
    {
        Debug.Log("Snapping to Rotation");
        this.transform.rotation = Quaternion.Euler(targetRotation);
    }

    /// <summary>
    /// Rotates by the target rotation
    /// </summary>
    private void QuaternionMultiplication()
    {
        Debug.Log("Multiplying Quaternions");
        this.transform.rotation = startRotation * Quaternion.Euler(targetRotation);
    }

    /// <summary>
    /// Points towards the target using angle axis
    /// </summary>
    private void AngleAxis()
    {
        Vector3 targetLookVector = target.transform.position - transform.position;
        Debug.DrawRay(transform.position, targetLookVector);
        float zRotation = Vector2.SignedAngle(Vector3.up, targetLookVector);
        print(zRotation);
        transform.rotation = Quaternion.AngleAxis(zRotation, Vector3.forward);
    }

    /*
    private void QuaternionSetLookRotation()
    {
        Debug.Log("Setting look rotation");
        Debug.Log(target.transform.position);
        Vector3 targetLookVector = target.transform.position - transform.position;
        Quaternion test = Quaternion.identity;
        test.SetLookRotation(targetLookVector, Vector3.left);
        this.transform.rotation = test;
    }
    */


}

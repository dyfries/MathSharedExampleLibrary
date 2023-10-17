using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationModeExample { RotateTowardsTarget, SnapToAngle, QuaternionMultiplication, QuaternionSetLookRotation, QuaternionSetFromToRotation }
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
    // Start is called before the first frame update
    void Start()
    {
        startRotation = Quaternion.identity;
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

        }
        else if (rotationMode == RotationModeExample.QuaternionMultiplication)
        {

        }
        else if (rotationMode == RotationModeExample.QuaternionSetLookRotation) 
        {
            
        }
        else if (rotationMode == RotationModeExample.QuaternionSetFromToRotation) 
        {
            
        }
    }


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
}

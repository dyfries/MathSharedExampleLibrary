using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public enum RotationMode { DirectControl, AngleAxisFixedStyle, AngleAxisAdditiveStyle, AngleAxisRotateOverTimeStyle,  AccelerationModel, EasingUsingLerp }
public class RotateArtInRangeExample : MonoBehaviour
{
    // rotate 2d art around the Z axis 5 degrees in either direction. 

    public float rotationRange = 5f; // degrees from origin

    public float currentRotation = 0;

    public Quaternion cachedQuat;
    public float rotateRate = 1f;

    public float inputX;

    public RotationMode rotationMode = RotationMode.DirectControl;

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        //
        //       
        if (rotationMode == RotationMode.DirectControl) {
            DirectControl();
        } else if (rotationMode == RotationMode.AngleAxisFixedStyle) {
            AngleAxisFixedStyle();
        } else if (rotationMode == RotationMode.AngleAxisAdditiveStyle) {
            AngleAxisAdditiveStyle();
        }else if (rotationMode == RotationMode.AngleAxisRotateOverTimeStyle) {
            AngleAxisRotateOverTimeStyle();
        }

    }

    void DirectControl() {
        Vector3 currentRot = transform.rotation.eulerAngles;
        currentRot.z = inputX * rotationRange * -1; // -1 to reverse rotation direction. 
        transform.rotation = Quaternion.Euler(currentRot);
    }

    void AngleAxisFixedStyle() {
        Quaternion TargetRot = Quaternion.AngleAxis(inputX * rotationRange * -1, Vector3.forward);

        // This will be fixed, note no T.dT here as we are working on a fixed rotation arc and don't want
        // to divide it. 
        transform.rotation = TargetRot;
    }

    void AngleAxisAdditiveStyle() {
        // Note I am adding the T.dT back in here as we are moving additively. 
        Quaternion TargetRot = Quaternion.AngleAxis(inputX * rotationRange * -1 * Time.deltaTime, Vector3.forward);

        transform.rotation = transform.rotation * TargetRot; // This will spin around and around. 
    }


    void AngleAxisRotateOverTimeStyle() {
        Quaternion TargetRot = Quaternion.AngleAxis(inputX * rotationRange * -1, Vector3.forward);

        cachedQuat = Quaternion.RotateTowards(transform.rotation, TargetRot, Time.deltaTime * rotateRate);

        // This will be fixed, note no T.dT here as we are working on a fixed rotation arc and don't want
        // to divide it. 
        transform.rotation = cachedQuat;
    }


    void AccelerationModel() {

    }

    void EasingUsingLerp() {

    }
}

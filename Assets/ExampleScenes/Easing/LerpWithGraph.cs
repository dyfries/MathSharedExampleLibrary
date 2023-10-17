using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpWithGraph : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float lerpDistance = 10.0f;
    [SerializeField] private float maxCurveTime = 1.0f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isCurrentlyLerping = false;
    private float currentTimer = 0.0f;

    public void LerpOverTime()
    {
        isCurrentlyLerping = true; // let us know we started
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(0, lerpDistance, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            LerpOverTime();
        }

        LerpUpdate();
    }

    private void LerpUpdate()
    {
        if (isCurrentlyLerping)
        {
            currentTimer += Time.deltaTime;
          
            Debug.Log("time: " + curve.Evaluate(currentTimer));
            if (currentTimer < maxCurveTime)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(currentTimer));
            }
        }

      
    }
}

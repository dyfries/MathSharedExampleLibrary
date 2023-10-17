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
    private float scaledCurrentTimer = 0.0f;

    public void LerpOverTime()
    {
        if (!isCurrentlyLerping)
        {
            isCurrentlyLerping = true; // let us know we started
            startPosition = transform.position;
            endPosition = transform.position + new Vector3(0, lerpDistance, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isCurrentlyLerping)
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
            scaledCurrentTimer = currentTimer / maxCurveTime; // Scaling the timer between 0 and maxTime

            if (currentTimer < maxCurveTime)
            {
                // Curve.Evaluate() gives the current value in the AnimationCurve at the scaled current time
                transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(scaledCurrentTimer));
            }
            else
            {
                isCurrentlyLerping = false;
            }
        }
    }
}

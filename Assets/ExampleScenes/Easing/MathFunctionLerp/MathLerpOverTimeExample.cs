using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLerpOverTimeExample : MonoBehaviour
{
    private float currentTimer = 0;

    [SerializeField] private float lerpTime = 1f;

    public float lerpDistance = 5f;

    private bool isCurrentlyLerping = false;
    private Vector3 startPosition;
    private Vector3 endPosition; // could do this different ways, I chose V3. 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isCurrentlyLerping)
        {
            LerpOverTime();
        }

        LerpUpdate();
    }

    public void LerpOverTime()
    {
        if (!isCurrentlyLerping)
        {
            isCurrentlyLerping = true; // let us know we started
            startPosition = transform.position;
            endPosition = transform.position + new Vector3(0, lerpDistance, 0);
        }
    }

    private void LerpUpdate()
    {
        if (isCurrentlyLerping)
        {
            currentTimer += Time.deltaTime;

            if (currentTimer > lerpTime)
            {
                isCurrentlyLerping = false;
            }
            else
            {
                float proportionFinished = currentTimer / lerpTime; // scales the time range between 0 to 1

                // sigmoid function : y = (k / 1 - euler's number ^ (-b * x))
                // Mathf.Exp(1) is euler's number (2.71...)
                float rate = 1 / (1 + Mathf.Pow(Mathf.Exp(1), -10 * proportionFinished + 5));

                // iterate through
                transform.position = Vector3.Lerp(startPosition, endPosition, rate);
            }
        }
    }
}

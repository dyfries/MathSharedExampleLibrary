using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LerpOverTimeExample : MonoBehaviour
{
    private float currentTimer = 0;

    [Range(0.0001f, 1000)]
    public float lerpTime = 1f;

    public float lerpDistance = 5f;

    private bool isCurrentlyLerping = false;
    private bool isOnCooldown = false;
    private Vector3 startPosition;
    private Vector3 endPosition; // could do this different ways, I chose V3. 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isOnCooldown && !isCurrentlyLerping) {
            LerpOverTime();
        }

        LerpUpdate();
    }

    public void LerpOverTime() {
        isCurrentlyLerping = true; // let us know we started
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(0, lerpDistance, 0);
    }
    public void LerpUpdate() {
        
        if (isCurrentlyLerping) {
            // increment timer. 
            currentTimer += Time.deltaTime;
            
            // are we done yet? 
            if (currentTimer > lerpTime){
                isCurrentlyLerping = false;
                isOnCooldown = true;
                // just using to demonstrate another technique, be careful with coroutines though! They can cause some real bugs if you are not careful. 
                StartCoroutine(ResetTimer());
            } else {
                float proportionFinished = currentTimer / lerpTime;

                // Insert a curve 
                // different response curve here .

                // iterate through
                transform.position = Vector3.Lerp(startPosition, endPosition, proportionFinished);
            
            }
        }
    }

    // And use an IEnumerator to reset just to show the technique
    private IEnumerator ResetTimer() {
        yield return new WaitForSeconds(3); // yeild makes the thread wait here until the timer expired, then the code picks up and continues. 
        transform.position = startPosition;
        isOnCooldown = false;
    }

}

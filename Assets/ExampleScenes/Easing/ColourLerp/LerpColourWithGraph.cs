using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColourWithGraph : MonoBehaviour
{
    [Header("SPRITE")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("CURVE")]
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float maxCurveTime = 1.0f;

    [Header("COLOURS")]
    [SerializeField] private Color startColour;
    [SerializeField] private Color endColour;

    private bool isCurrentlyLerping = false;
    private float currentTimer = 0.0f;

    public void LerpOverTime()
    {
        if (!isCurrentlyLerping)
        {
            isCurrentlyLerping = true; // let us know we started
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

            if (currentTimer < maxCurveTime)
            {
                float scaledTime = currentTimer / maxCurveTime;

                sprite.color = Color.Lerp(startColour, endColour, curve.Evaluate(scaledTime));
            }
            else
            {
                currentTimer = 0;
                isCurrentlyLerping = false;

                // swap the colours so it can fade in reverse on next lerp
                Color tempColor = startColour;
                startColour = endColour;
                endColour = tempColor;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class ScaleChanger : MonoBehaviour
{
    public Vector3 minScale = new Vector3(1f, 1f, 1f); // Initial scale (scale in)
    public Vector3 maxScale = new Vector3(2f, 2f, 2f); // Target scale (scale out)
    public float duration = 1f; // Duration of each scale animation
    public float interval = 2f; // Time between scale in and scale out
    public float totalDuration = 10f; // Total duration before destruction

    private void Start()
    {
        // Start the scaling loop and destruction timer
        StartCoroutine(ScalingAndDestruction());
    }

    private IEnumerator ScalingAndDestruction()
    {
        float elapsedTime = 0f;

        while (elapsedTime < totalDuration)
        {
            // Start scaling out
            LeanTween.scale(gameObject, maxScale, duration).setEase(LeanTweenType.easeInOutQuad);

            // Wait for the scaling out to complete
            yield return new WaitForSeconds(duration);

            // Wait for the interval
            yield return new WaitForSeconds(interval);

            // Start scaling in
            LeanTween.scale(gameObject, minScale, duration).setEase(LeanTweenType.easeInOutQuad);

            // Wait for the scaling in to complete
            yield return new WaitForSeconds(duration);

            // Wait for the interval
            yield return new WaitForSeconds(interval);

            // Update elapsed time
            elapsedTime += 2 * (duration + interval);
        }

        // Destroy the GameObject after the total duration
        Destroy(gameObject);
    }
}

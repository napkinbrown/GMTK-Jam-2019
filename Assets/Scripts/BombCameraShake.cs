using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCameraShake : MonoBehaviour
{
    public Camera attachedCamera;

    public float initialJoltDuration;
    public float initialJoltIntensity;

    public float shakeDuration;
    public float shakeIntensity;
    public float shakeIntensityDropoff;

    /** What the camera should be reset to after it's done shaking */
    private Vector3 startPosition;

    public void onExplosionEvent()
    {
        startPosition = attachedCamera.transform.localPosition;
        this.StartCoroutine(cameraJolt());
    }

    IEnumerator cameraJolt()
    {
        float endTime = Time.time + initialJoltDuration;
        while (Time.time < endTime)
        {
            randomlyTranslateCamera(startPosition, initialJoltIntensity);
            yield return null;
        }

        StartCoroutine(cameraShake());
    }

    IEnumerator cameraShake()
    {
        float endTime = Time.time + shakeDuration;

        float currentIntesity = shakeIntensity;
        while (Time.time < endTime)
        {
            randomlyTranslateCamera(startPosition, currentIntesity);
            currentIntesity = Mathf.Lerp(currentIntesity, 0, shakeIntensityDropoff);
            yield return null;
        }

        resetCamera();
    }

    public void resetCamera()
    {
        attachedCamera.transform.localPosition = startPosition;
    }

    public void randomlyTranslateCamera(Vector3 position, float intensity)
    {
        attachedCamera.transform.localPosition = position + Random.insideUnitSphere * intensity;
    }
}

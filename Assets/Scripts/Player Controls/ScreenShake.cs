using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Vector3 position;
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeAmount;

    public void Awake()
    {
        AutoDestroy.OnImpact += ShakeScreen;
    }

    public void Start()
    {
        position = transform.position;
    }

    public void OnDestroy()
    {
        AutoDestroy.OnImpact -= ShakeScreen;
    }

    private void ShakeScreen()
    {
        StartCoroutine(Shake(shakeTime, shakeAmount));
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        position = transform.localPosition;

        float timeElapsed = 0.0f;

        while(timeElapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1.0f, 1.0f) * magnitude;
            float y = UnityEngine.Random.Range(-1.0f, 1.0f) * magnitude;

            transform.localPosition = new Vector3(x, y, position.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = position;
    }
}

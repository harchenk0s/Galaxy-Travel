using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] [Range(0f,10f)] private float _duration;
    [SerializeField] [Range(0f, 20f)] private float _magnitude;
    [SerializeField] [Range(0f, 1000f)] private float _noize;

    private IEnumerator _shakeCoroutine = null;
    private Animator _animator;
    private bool _isAnimatorExist = false;

    private void Awake()
    {
        _isAnimatorExist = TryGetComponent(out _animator);
    }

    public void Shake()
    {
        Shake(_duration, _magnitude, _noize);
    }

    public void Shake(float duration, float magnitude, float noize)
    {
        if (_isAnimatorExist)
            _animator.enabled = false;

        if(_shakeCoroutine != null)
            StopCoroutine(_shakeCoroutine);

        _shakeCoroutine = ShakeCoroutine(duration, magnitude, noize);
        StartCoroutine(_shakeCoroutine);
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude, float noize)
    {
        Vector3 startPosition = transform.localPosition;
        float elapsed = 0f;
        Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
        Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

        while (elapsed < duration)
        {
            Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
            Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);

            Vector2 currentNoizeValue = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y),
                Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));

            Vector2 cameraPostionDelta = new Vector2(Mathf.Lerp(-magnitude, magnitude, currentNoizeValue.x),
                Mathf.Lerp(-magnitude, magnitude, currentNoizeValue.y));
            
            transform.localPosition = startPosition + (Vector3)cameraPostionDelta;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPosition;
        _shakeCoroutine = null;
        _animator.enabled = true;
    }
}

using System.Collections;
using UnityEngine;

public class BossDefenceHelper : MonoBehaviour
{
    public void StartDelay(float delay, System.Action onComplete)
    {
        StartCoroutine(DelayCoroutine(delay, onComplete));
    }

    private IEnumerator DelayCoroutine(float delay, System.Action onComplete)
    {
        yield return new WaitForSeconds(delay);
        onComplete?.Invoke();
    }
}
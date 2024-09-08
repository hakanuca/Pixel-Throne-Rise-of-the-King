using UnityEngine;
using System.Collections;
using Cinemachine;

public class SceneStartAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] keys;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float startSize = 40f;
    [SerializeField] private float endSize = 7f;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float zoomSpeed = 1f; 
    [SerializeField] private float transformSpeed = 1f; 
    [SerializeField] private ParticleSystem playerInvisibleEffect;
    private bool hasAnimationPlayed = false; 

    private void Start()
    {
        if (!hasAnimationPlayed) 
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }

            foreach (GameObject key in keys)
            {
                key.SetActive(false);
            }

            if (player != null)
            {
                player.SetActive(false);
            }

            if (cam != null)
            {
                cam.m_Lens.OrthographicSize = startSize;
                StartCoroutine(ChangeOrthoSize());
            }
        }
    }

    private IEnumerator ChangeOrthoSize()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, elapsedTime / duration);
            if (player != null)
            {
                player.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, elapsedTime / duration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.m_Lens.OrthographicSize = endSize;
        if (player != null)
        {
            player.transform.position = endPoint.position;
        }
        OnAnimationEnd();
    }

    public void OnAnimationEnd()
    {
        ActivateAllObjects();
        if (player != null)
        {
            playerInvisibleEffect.Play();
            player.SetActive(true);
            
        }
        hasAnimationPlayed = true; 
    }

    public void ActivateAllObjects()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        foreach (GameObject key in keys)
        {
            key.SetActive(true);
        }
    }
}
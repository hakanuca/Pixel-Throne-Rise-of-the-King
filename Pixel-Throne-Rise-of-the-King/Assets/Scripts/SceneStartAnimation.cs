using UnityEngine;

public class SceneStartAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] keys;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        foreach (GameObject key in keys)
        {
            key.SetActive(false);
        }
    }
    
    public void OnAnimationEnd()
    {
        ActivateAllObjects();
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
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // Assign the GameObject to spawn in the Inspector
    [SerializeField] private float spawnInterval = 5f; // Time interval between spawns
    [SerializeField] private float objectLifetime = 10f; // Time before the spawned object is destroyed

    private void Start()
    {
        // Start the spawning process
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        if (objectToSpawn != null)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            StartCoroutine(DestroyAfterTime(spawnedObject, objectLifetime));
        }
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}

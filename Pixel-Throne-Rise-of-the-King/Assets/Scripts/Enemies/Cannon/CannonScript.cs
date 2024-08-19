using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bullet;
    [SerializeField] private AudioSource audioSource;  // Serialize the AudioSource field
    float timeBetween;
    public float startTimeBetween;

    void Start()
    {
        timeBetween = startTimeBetween;
    }

    void Update()
    {
        if (timeBetween <= 0)
        {
            FireCannon();
            timeBetween = startTimeBetween;
        }
        else
        {
            timeBetween -= Time.deltaTime;
        }
    }

    void FireCannon()
    {
        Instantiate(bullet, firepoint.position, firepoint.rotation);

        // Play the sound when the cannon fires
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}

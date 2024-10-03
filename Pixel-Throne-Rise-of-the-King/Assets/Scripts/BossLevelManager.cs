using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private GameObject[] startBlocks;

    private void Start()
    {
        for (int i = 0; i < startBlocks.Length; i++)
        {
            startBlocks[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (bossHealth == null || bossHealth.currentHealth <= 0)
        {
            OpenDoorsOnBossDeath();
        }
    }

    private void OpenDoorsOnBossDeath()
    {
        door.SetActive(true);
        for (int i = 0; i < startBlocks.Length; i++)
        {
            startBlocks[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < startBlocks.Length; i++)
            {
                startBlocks[i].SetActive(true);
            }
        }
    }
}
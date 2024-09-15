using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject blocks;
    [SerializeField] private BossHealth bossHealth;

    private void Start()
    {
        bossHealth.OnDeath(OpenDoorsOnBossDeath);
    }

    private void OpenDoorsOnBossDeath()
    {
        door.SetActive(false);
        blocks.SetActive(false);
    }
}
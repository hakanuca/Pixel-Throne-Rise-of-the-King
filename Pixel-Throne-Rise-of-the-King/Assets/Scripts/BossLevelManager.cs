using System;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject blocks;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private GameObject[] startBlocks;

    private void Start()
    {
        bossHealth.OnDeath(OpenDoorsOnBossDeath);
    }

    private void OpenDoorsOnBossDeath()
    {
        door.SetActive(false);
        blocks.SetActive(false);
        startBlocks[0].SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startBlocks[0].SetActive(true);
    }
}
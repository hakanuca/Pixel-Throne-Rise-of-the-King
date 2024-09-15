using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject blocks;
    private void OpenDoorsOnBossDeath()
    {
        door.SetActive(false);
        blocks.SetActive(false);
    }
}

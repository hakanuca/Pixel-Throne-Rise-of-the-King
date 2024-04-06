using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
  [SerializeField] private float addHealth = 1.3f;


  public void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      collision.GetComponent<Health>().AddHealth(addHealth);
      gameObject.SetActive(false);

    }
  }

}

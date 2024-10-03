using UnityEngine;

public class HealthBuff : MonoBehaviour
{
  [SerializeField] private float addHealth = 1.3f;
  private Health health;


  public void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player" && collision.GetComponent<Health>().currentHealth < collision.GetComponent<Health>().startingHealth)
    {
      collision.GetComponent<Health>().AddHealth(addHealth);
      Destroy(gameObject);
    }
  }

}

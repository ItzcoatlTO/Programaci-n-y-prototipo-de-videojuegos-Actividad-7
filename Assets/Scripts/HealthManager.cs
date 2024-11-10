using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject explosionPrefab;

    public static int enemiesDestroyed = 0; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} ha recibido {damage} de da�o. Vida actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Explode();
            if (gameObject.CompareTag("Enemy"))
            {
                enemiesDestroyed++; 
                Debug.Log($"Enemigos derribados: {enemiesDestroyed}");
            }
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        Debug.Log($"{gameObject.name} ha sido destruido.");
    }
}

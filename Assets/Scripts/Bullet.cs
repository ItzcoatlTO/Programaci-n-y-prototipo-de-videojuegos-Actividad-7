using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20; // Daño que inflige cada bala

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto impactado tiene el componente HealthManager
        HealthManager health = collision.gameObject.GetComponent<HealthManager>();
        if (health != null)
        {
            // Reduce la vida del objeto impactado
            health.TakeDamage(damage);
        }

        // Destruye la bala tras el impacto
        Destroy(gameObject);
    }
}

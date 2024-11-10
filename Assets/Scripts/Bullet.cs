using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20; 

    void OnCollisionEnter(Collision collision)
    {
        HealthManager health = collision.gameObject.GetComponent<HealthManager>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

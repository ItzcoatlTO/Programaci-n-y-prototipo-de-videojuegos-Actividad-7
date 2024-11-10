using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public GameObject bulletPrefab; 
    public Transform[] shootPoints; 
    public float shootForce = 10f; 
    public float shootInterval = 2f; 
    public AudioSource audioSource; 
    public AudioClip shootSound; 

    private float shootTimer;

    void Start()
    {
        
        shootTimer = shootInterval;
    }

    void Update()
    {
      
        FollowPlayer();

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootInterval; 
        }
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    void Shoot()
    {
        if (player != null)
        {
            foreach (Transform shootPoint in shootPoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                Vector3 direction = (player.position - shootPoint.position).normalized;

                bullet.transform.forward = direction;

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(direction * shootForce, ForceMode.Impulse);
                }
            }

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject bulletPrefab; 
    public Transform shootPoint; 
    public float shootForce = 10f;
    public AudioSource audioSource; 
    public AudioClip shootSound; 
    public Transform enemy; 

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null && enemy != null)
        {
          
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            Vector3 direction = (enemy.position - shootPoint.position).normalized;

           
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * shootForce, ForceMode.Impulse);
            }

           
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }
}

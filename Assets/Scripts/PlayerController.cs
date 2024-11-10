using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform shootPoint; // Punto desde donde se dispara
    public float shootForce = 10f; // Fuerza del disparo
    public AudioSource audioSource; // Componente de AudioSource para el sonido del disparo
    public AudioClip shootSound; // Clip de audio para el disparo
    public Transform enemy; // Referencia al enemigo

    void Update()
    {
        // Llama a las funciones de movimiento y disparo en cada frame
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Move()
    {
        // Captura las entradas de movimiento (horizontal y vertical)
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha
        float verticalInput = Input.GetAxis("Vertical"); // W/S o flechas arriba/abajo

        // Movimiento en el plano X-Z
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null && enemy != null)
        {
            // Instancia el proyectil en el punto de disparo
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            // Calcula la dirección hacia el enemigo
            Vector3 direction = (enemy.position - shootPoint.position).normalized;

            // Agrega fuerza en la dirección del enemigo
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * shootForce, ForceMode.Impulse);
            }

            // Reproduce el sonido de disparo
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }
}

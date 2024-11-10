using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 3.0f; // Velocidad de movimiento
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform[] shootPoints; // Array de puntos de disparo (3 puntos)
    public float shootForce = 10f; // Fuerza del disparo
    public float shootInterval = 2f; // Intervalo de disparo en segundos
    public AudioSource audioSource; // Componente de AudioSource para el sonido de disparo
    public AudioClip shootSound; // Clip de audio para el disparo

    private float shootTimer; // Temporizador para controlar el disparo

    void Start()
    {
        // Configura el temporizador de disparo
        shootTimer = shootInterval;
    }

    void Update()
    {
        // Movimiento para seguir al jugador
        FollowPlayer();

        // Temporizador para el disparo
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootInterval; // Reinicia el temporizador
        }
    }

    void FollowPlayer()
    {
        // Verifica que el jugador esté asignado
        if (player != null)
        {
            // Calcula la dirección hacia el jugador, pero sin rotar al enemigo
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve al enemigo hacia el jugador, sin cambiar su rotación
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    void Shoot()
    {
        // Verifica que el jugador esté asignado
        if (player != null)
        {
            // Recorre cada punto de disparo y dispara hacia el jugador
            foreach (Transform shootPoint in shootPoints)
            {
                // Instancia el proyectil en el punto de disparo
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                // Calcula la dirección hacia el jugador
                Vector3 direction = (player.position - shootPoint.position).normalized;

                // Apunta la bala en la dirección del jugador
                bullet.transform.forward = direction;

                // Agrega fuerza en la dirección del jugador
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(direction * shootForce, ForceMode.Impulse);
                }
            }

            // Reproduce el sonido de disparo
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }
}

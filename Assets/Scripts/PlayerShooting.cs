using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform shootPoint; // Punto desde donde se dispara
    public float shootForce = 10f; // Fuerza del disparo
    public AudioSource audioSource; // Componente de AudioSource para el sonido del disparo
    public AudioClip shootSound; // Clip de audio para el disparo

    void Update()
    {
        // Dispara cuando se presiona la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Verifica que se hayan asignado el prefab, el punto de disparo y el sonido
        if (bulletPrefab != null && shootPoint != null)
        {
            // Instancia el proyectil en el punto de disparo con la misma rotación del jugador
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

            // Agrega una fuerza al proyectil para que se mueva
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            }

            // Reproducir el sonido de disparo
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
        else
        {
            Debug.LogWarning("Prefab del proyectil o punto de disparo no están asignados.");
        }
    }
}

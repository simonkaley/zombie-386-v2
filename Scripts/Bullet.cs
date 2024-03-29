using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private float maxDistance = 10f; // Maximum distance the bullet can travel

    void Update()
    {
        MoveBullet();
    }

    public void SetDirection(Vector3 shootDirection, float bulletSpeed)
    {
        direction = shootDirection;
        speed = bulletSpeed;

    }

    // Method to move the bullet over time
    private void MoveBullet()
    {
        // Calculate new position based on the direction and speed
        Vector3 newPosition = transform.position + direction.normalized * speed * Time.deltaTime;

        // Update the bullet's position
        transform.position = newPosition;

        // Check if the bullet has traveled the maximum distance
        if (transform.parent != null && Vector3.Distance(transform.position, transform.parent.position) >= maxDistance)
        {
            // Deactivate the bullet instead of destroying it
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Handle collision with enemies and obstacles
        if (collider.tag == "Enemy")
        {
            gameObject.SetActive(false);
            GameObject enemy = collider.gameObject;

            // Check if the enemy is still active before attempting to handle or destroy it
            if (enemy != null && enemy.activeSelf)
            {
                Destroy(enemy); // Destroy the enemy
            }
        }

        if (collider.tag == "Obstacle")
        {
            // Deactivate the bullet instead of destroying it
            gameObject.SetActive(false);
        }
    }
}

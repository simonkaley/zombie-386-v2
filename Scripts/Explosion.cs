using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private CircleCollider2D explosionCollider;
    private AudioPlayer audioPlayer;

    [SerializeField] private AudioClip explosionSound; // Sound effect for the explosion
    [SerializeField] private float explosionDuration = 0.1f;

    void Start()
    {
        // Get the CircleCollider2D component attached to the explosion object
        explosionCollider = GetComponent<CircleCollider2D>();

        audioPlayer = FindObjectOfType<AudioPlayer>();

        // Trigger the explosion immediately
        Explode();
    }

    void Explode()
    {
        // Play the explosion sound effect
        audioPlayer.playExplodeSounds();

        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionCollider.radius);

        // Loop through all colliders
        foreach (Collider2D col in colliders)
        {
            // Check if the collider belongs to an enemy, health pack, machine gun, or shotgun
            if (col.CompareTag("Enemy") || col.CompareTag("Health") || col.CompareTag("machineGun") || col.CompareTag("shotgun"))
            {
                // Destroy the object
                Destroy(col.gameObject);
            }
            if (col.CompareTag("Player"))
            {
                PlayerData playerData = col.gameObject.GetComponent<PlayerData>();
                if (playerData != null)
                {
                    playerData.handleExplosion();
                }
            }
        }

        // Destroy the explosion object after the specified duration
        Destroy(gameObject, explosionDuration);
    }
}

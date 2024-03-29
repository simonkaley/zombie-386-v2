using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private control audioControl; // Reference to the control script

    [SerializeField]
    // Array to hold death sound effects
    public AudioClip[] deathSounds;

    [SerializeField]
    // Array to hold shoot sound effects
    public AudioClip[] shootSounds;

    [SerializeField]
    public AudioClip[] explodeSounds;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Find the control script in the scene
        audioControl = FindObjectOfType<control>();

        if (audioControl!=null)
        {
            SetVolume(audioControl.getSoundEffectVolume());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to set the volume of the audio player
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    // Method to play random death sound
    public void enemyDeathSounds()
    {
        if (deathSounds.Length > 0)
        {
            // Generate a random index within the range of the array
            int randomIndex = Random.Range(0, deathSounds.Length);

            // Play the selected sound effect
            if (audioSource != null && deathSounds[randomIndex] != null)
            {
                audioSource.PlayOneShot(deathSounds[randomIndex]);
            }
        }
    }

    // Method to play random shoot sound
    public void playerShootSounds()
    {
        if (shootSounds.Length > 0)
        {
            // Generate a random index within the range of the array
            int randomIndex = Random.Range(0, shootSounds.Length);

            // Play the selected sound effect
            if (audioSource != null && shootSounds[randomIndex] != null)
            {
                audioSource.PlayOneShot(shootSounds[randomIndex]);
            }
        }
    }

    // Method to play random explode sound
    public void playExplodeSounds()
    {
        if (shootSounds.Length > 0)
        {
            // Generate a random index within the range of the array
            int randomIndex = Random.Range(0, explodeSounds.Length);

            // Play the selected sound effect
            if (audioSource != null && explodeSounds[randomIndex] != null)
            {
                audioSource.PlayOneShot(explodeSounds[randomIndex]);
            }
        }
    }
}

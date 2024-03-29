using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class volumeSoundEffectSlider : MonoBehaviour
{
    public Slider volumeSlider;
    private control audioControl; // Reference to the control script
    private AudioSource soundEffectAudioSource; // Reference to the AudioSource for the sound effect

    public AudioClip volumeChangeSound; // Sound effect to play when volume changes

    private bool volumeChangeInProgress = false; // Flag to track if volume change is in progress

    void Start()
    {
        // Find the control script in the scene
        audioControl = FindObjectOfType<control>();

        if (audioControl == null)
        {
            Debug.LogError("Control script not found in the scene.");
            return;
        }

        // Get the AudioSource component attached to this GameObject
        soundEffectAudioSource = GetComponent<AudioSource>();

        // Optionally, you can set the slider's initial value to the current volume
        volumeSlider.value = audioControl.getSoundEffectVolume();

        Debug.Log("Initial Slider Value: " + volumeSlider.value);
    }

    // Method called when the slider value changes
    public void OnVolumeChange()
    {
        if (audioControl != null)
        {
            // Set global volume using the control script
            audioControl.setSoundEffectVolume(volumeSlider.value);

            // Set the volume of the sound effect audio source to match the new volume
            if (soundEffectAudioSource != null)
            {
                soundEffectAudioSource.volume = volumeSlider.value;
            }

            if (!volumeChangeInProgress)
            {
                // Set the flag to indicate volume change is in progress
                volumeChangeInProgress = true;

                // Play the volume change sound effect after a short delay
                StartCoroutine(PlayVolumeChangeSoundAfterDelay());
            }
        }
        Debug.Log("Changed");
    }

    // Coroutine to play the volume change sound effect after a delay
    private IEnumerator PlayVolumeChangeSoundAfterDelay()
    {
        // Wait for a short delay
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed

        // Play the volume change sound effect if available
        if (soundEffectAudioSource != null && volumeChangeSound != null)
        {
            soundEffectAudioSource.PlayOneShot(volumeChangeSound);
        }

        // Reset the flag after playing the sound effect
        volumeChangeInProgress = false;
    }
}

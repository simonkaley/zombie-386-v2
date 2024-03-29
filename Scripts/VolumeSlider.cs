using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;
    private control audioControl; // Reference to the control script

    void Start()
    {
        // Find the control script in the scene
        audioControl = FindObjectOfType<control>();

        if (audioControl == null)
        {
            Debug.LogError("Control script not found in the scene.");
            return;
        }

        // Add listener to the slider's value change event
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });

        // Optionally, you can set the slider's initial value to the current volume
        volumeSlider.value = audioControl.getVolume();
    }

    // Method called when the slider value changes
    public void OnVolumeChange()
    {
        if (audioControl != null)
        {
            // Set global volume using the control script
            audioControl.SetGlobalVolume(volumeSlider.value, true); // Assuming loop is set to true
            
        }
    }
}
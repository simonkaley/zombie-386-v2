using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class control : MonoBehaviour
{
    SaveData saveData;

    public static int killScore = 0;
    private static control instance;

    private static float soundEffectVolume=1;

    // List to hold references to all AudioSource components in the scene
    private List<AudioSource> allAudioSources = new List<AudioSource>();

    private void Awake()
    {
        // If the control is null, assign this instance to it
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            saveData = ScriptableObject.CreateInstance<SaveData>();

            if (File.Exists(Application.persistentDataPath + "/CurScore.dat"))
            {
                Debug.Log("Loading from " + Application.persistentDataPath + "/CurScore.dat");
                JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/CurScore.dat"), saveData);
                killScore = saveData.killScore;
            }

            // Find all AudioSource components in the scene and add them to the list
            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                allAudioSources.Add(audioSource);
            }

            SetGlobalVolume(1f, true); // Set looping to true
        }
        // If a control already exists and it's not this instance, destroy this control
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

    }

    void OnDestroy()
    {
        if (instance == this)
        {
            ShuttingDown();
        }
    }

    public void ShuttingDown()
    {
        saveData.killScore = killScore;
        File.WriteAllText(Application.persistentDataPath + "/CurScore.dat", JsonUtility.ToJson(saveData));
    }

    // Method to adjust the volume of all audio sources and set looping
    public void SetGlobalVolume(float volume, bool loop)
    {
        // Clamp the volume to the range [0, 1]
        volume = Mathf.Clamp01(volume);

        // Set the volume of each audio source in the list
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = volume;
            audioSource.loop = loop; // Set looping
        }
    }

    public float getVolume()
    {
        // Calculate the average volume of all audio sources
        float totalVolume = 0;
        foreach (AudioSource audioSource in allAudioSources)
        {
            totalVolume += audioSource.volume;
        }
        return allAudioSources.Count > 0 ? totalVolume / allAudioSources.Count : 0;
    }
    public float getSoundEffectVolume()
    {
        return soundEffectVolume;
    }
    public void setSoundEffectVolume(float getVolume)
    {
        soundEffectVolume=getVolume;
    }
}
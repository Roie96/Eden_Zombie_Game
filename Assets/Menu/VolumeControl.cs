using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VolumeControl : MonoBehaviour
{
    private AudioSource[] audioSources;
    public Slider volumeSlider; // Reference to the volume slider UI element


    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioSources = FindObjectsOfType<AudioSource>();
        UpdateVolumeSlider(); // Update the volume slider to reflect the actual volume

    }

  public void SetVolume(float volume)
    {
        Debug.Log("Setting volume to: " + volume);
        foreach (AudioSource audioSource in audioSources) {
            audioSource.volume = volume;
            }
    }

 public void UpdateVolumeSlider()
    {
        // Get the volume from any of the audio sources (assuming all have the same volume)
        float volume = audioSources[0].volume;
        // Set the volume slider value to reflect the actual volume
        volumeSlider.value = volume;
    }
}
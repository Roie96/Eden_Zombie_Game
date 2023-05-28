using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VolumeControl : MonoBehaviour
{
    private AudioSource[] audioSources;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioSources = FindObjectsOfType<AudioSource>();
    }

  public void SetVolume(float volume)
{
    Debug.Log("Setting volume to: " + volume);
    foreach (AudioSource audioSource in audioSources)
    {
        audioSource.volume = volume;
    }
}
}
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private Slider volumeSlider;
    private AudioSource[] audioSources;

    private void Start()
    {
        Cursor.visible = true;
        volumeSlider = GetComponent<Slider>();
        audioSources = FindObjectsOfType<AudioSource>();
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

         // Set the slider's value to the initial volume
        float initialVolume = audioSources[0].volume; // Assuming the first audio source represents the overall game volume
        volumeSlider.value = initialVolume;
    }
    void Update()
    {
        Cursor.visible = true;
    }

    private void OnVolumeChanged(float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }
}
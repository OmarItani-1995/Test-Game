using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Prefab : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private System.Action<Audio_Prefab> _onAudioDone;
    private float timer;
    private float maxTime;
    public Audio_Prefab Play(AudioClip clip, System.Action<Audio_Prefab> onAudioDone)
    {
        audioSource.clip = clip;
        _onAudioDone = onAudioDone;
        timer = 0f;
        maxTime = clip.length + 0.1f;
        audioSource.Play();
        return this;
    }
    
    public Audio_Prefab SetVolume(float volume)
    {
        audioSource.volume = volume;
        return this;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            _onAudioDone?.Invoke(this);
        }
    }
}

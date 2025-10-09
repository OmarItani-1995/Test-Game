using System.Collections;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public class Audio_Manager : MonoBehaviour, IAudioManager
{
    [SerializeField] private GameObject audioPrefab;
    [SerializeField] private SerializableDictionaryBase<Audio_ClipType, AudioClip> audioClips;
    private Pool<Audio_Prefab> _audioPool;

    void Awake()
    {
        DI.Register<IAudioManager>(this);
    }
    void Start()
    {
        _audioPool = new Pool<Audio_Prefab>(OnCreateAudioPrefab, 5);
    }

    private Audio_Prefab OnCreateAudioPrefab()
    {
        Audio_Prefab audio = Instantiate(audioPrefab).GetComponent<Audio_Prefab>();
        audio.transform.SetParent(transform);
        return audio;
    }

    public void PlayAudio(Audio_ClipType clipType, float volume = 1)
    {
        _audioPool
            .Get()
            .Play(audioClips[clipType], OnAudioComplete)
            .SetVolume(volume);
    }

    public void OnAudioComplete(Audio_Prefab audio)
    {
        _audioPool.ReturnToPool(audio);
    }
}

public enum Audio_ClipType
{
    Card_Flip,
    Deal_Card,
}

public interface IAudioManager
{
    void PlayAudio(Audio_ClipType clipType, float volume = 1);
}

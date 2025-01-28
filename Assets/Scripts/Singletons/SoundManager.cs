using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SoundEntry
{
    public string Key;
    public AudioClip Value;
}

public class SoundManager : MonoBehaviourSingletonPersistent<SoundManager>
{
    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    
    public List<SoundEntry> SoundsList;
    private Dictionary<string, AudioClip> soundsDictionary;
    
     
    new void Awake()
    {
        base.Awake();
        // Convert the List to a Dictionary at runtime for easier access
        soundsDictionary = new Dictionary<string, AudioClip>();
        foreach (var entry in SoundsList)
        {
            if (!soundsDictionary.ContainsKey(entry.Key))
            {
                soundsDictionary.Add(entry.Key, entry.Value);
            }
        }
    }
    
    private AudioClip GetSound(string key)
    {
        print(soundsDictionary.Count);
        AudioClip clip = soundsDictionary.TryGetValue(key, out var value) ? value : null;
        if (clip == null)
        {
            Debug.LogError($"Sound {key} not found");
        }
        return clip;
    }

    public void Play(string keyName)
    {
        EffectsSource.clip = GetSound(keyName);
        EffectsSource.Play();
    }

    public void PlayMusic(string keyName)
    {
        MusicSource.clip = GetSound(keyName);
        MusicSource.Play();
    }
	
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviourSingletonPersistent<SoundManager>
{
    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    public Dictionary<string, AudioClip> Sounds;

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        EffectsSource.clip = clips[randomIndex];
        EffectsSource.Play();
    }
	
}